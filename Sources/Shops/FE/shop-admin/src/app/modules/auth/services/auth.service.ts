import { Injectable, OnDestroy } from '@angular/core';
import { Observable, BehaviorSubject, of, Subscription } from 'rxjs';
import { map, catchError, switchMap, finalize } from 'rxjs/operators';
import { UserModel } from '../models/user.model';
import { AuthModel } from '../models/auth.model';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { AuthHTTPService } from './auth-http/auth-http.service';

export type UserType = UserModel | undefined;

@Injectable({
  providedIn: 'root',
})
export class AuthService implements OnDestroy {
  // private fields
  private unsubscribe: Subscription[] = []; // Read more: => https://brianflove.com/2016/12/11/anguar-2-unsubscribe-observables/
  private authLocalStorageToken = `${environment.appVersion}-${environment.USERDATA_KEY}`;
  private tokenKey = `${environment.appVersion}-${environment.TOKEN_KEY}`;

  // public fields

  isLoading$: Observable<boolean>;

  currentUser$: Observable<UserType>;
  currentUserSubject: BehaviorSubject<UserType>;

  currentToken$: Observable<any>;
  currentTokenSubject: BehaviorSubject<any>;

  isLoadingSubject: BehaviorSubject<boolean>;

  get currentUserValue(): UserType {
    if (
      this.currentUserSubject.value === null ||
      this.currentUserSubject.value === undefined
    )
      this.currentUserSubject = new BehaviorSubject<any>(
        JSON.parse(localStorage.getItem(this.authLocalStorageToken)!)
      );
    return this.currentUserSubject.value;
  }

  set currentUserValue(user: UserType) {
    this.currentUserSubject.next(user);
  }

  get currentTokenValue(): any {
    if (
      this.currentTokenSubject.value === null ||
      this.currentTokenSubject.value === undefined
    )
      this.currentTokenSubject = new BehaviorSubject<any>(
        JSON.parse(localStorage.getItem(this.tokenKey)!)
      );
    return this.currentTokenSubject.value;
  }

  set currentTokenValue(tokenData: any) {
    this.currentTokenSubject.next(tokenData);
  }

  constructor(
    private authHttpService: AuthHTTPService,
    private router: Router
  ) {
    this.isLoadingSubject = new BehaviorSubject<boolean>(false);

    this.currentUserSubject = new BehaviorSubject<UserType>(undefined);
    this.currentUser$ = this.currentUserSubject.asObservable();

    this.currentTokenSubject = new BehaviorSubject<any>(undefined);
    this.currentToken$ = this.currentTokenSubject.asObservable();

    this.isLoading$ = this.isLoadingSubject.asObservable();

    // const subscr = this.getUserByToken().subscribe();
    // this.unsubscribe.push(subscr);
  }

  // public methods
  login(taxcode: string,username: string, password: string): Observable<UserType> {
    this.isLoadingSubject.next(true);
    return this.authHttpService.login(taxcode,username, password).pipe(
      map((value: any) => {
        const result = this.setTokenFromLocalStorage(value.value);
        this.currentTokenSubject.next(value.value);
        return result;
      }),
      switchMap(() => this.getUserByToken()),
      catchError((err) => {
        console.error('err', err);
        return of(undefined);
      }),
      finalize(() => this.isLoadingSubject.next(false))
    );
  }

  logout() {
    localStorage.removeItem(this.authLocalStorageToken);
    localStorage.removeItem(this.tokenKey);
    this.router.navigate(['/auth/login'], {
      queryParams: {},
    });
  }

  getUserByToken(): Observable<UserType> {
    const auth = this.getTokenFromLocalStorage();
    if (!auth || !auth.authToken) {
      return of(undefined);
    }

    this.isLoadingSubject.next(true);
    return this.authHttpService.getUserByToken(auth.authToken).pipe(
      map((response: any) => {
        if (response.isSuccess) {
           this.setAuthFromLocalStorage(response.value);
          this.currentUserSubject.next(response.value);
        } else {
          this.logout();
        }
        return response.value;
      }),
      finalize(() => this.isLoadingSubject.next(false))
    );
  }

  // need create new user then login
  registration(user: UserModel): Observable<any> {
    this.isLoadingSubject.next(true);
    return this.authHttpService.createUser(user).pipe(
      map(() => {
        this.isLoadingSubject.next(false);
      }),
      switchMap(() => this.login(user.taxCode,user.email, user.password)),
      catchError((err) => {
        console.error('err', err);
        return of(undefined);
      }),
      finalize(() => this.isLoadingSubject.next(false))
    );
  }

  forgotPassword(email: string): Observable<boolean> {
    this.isLoadingSubject.next(true);
    return this.authHttpService
      .forgotPassword(email)
      .pipe(finalize(() => this.isLoadingSubject.next(false)));
  }

  // private methods
  private setAuthFromLocalStorage(auth: AuthModel): boolean {
    // store auth authToken/refreshToken/epiresIn in local storage to keep user logged in between page refreshes
    if (auth) {
      localStorage.setItem(this.authLocalStorageToken, JSON.stringify(auth));
      return true;
    }
    return false;
  }

  private getAuthFromLocalStorage(): AuthModel | undefined {
    try {
      const lsValue = localStorage.getItem(this.authLocalStorageToken);
      if (!lsValue) {
        return undefined;
      }

      const authData = JSON.parse(lsValue);
      return authData;
    } catch (error) {
      console.error(error);
      return undefined;
    }
  }

  // private methods
  private setTokenFromLocalStorage(tokenData: any): boolean {
    // store auth authToken/refreshToken/epiresIn in local storage to keep user logged in between page refreshes
    if (tokenData && tokenData.authToken) {
      localStorage.setItem(this.tokenKey, JSON.stringify(tokenData));
      return true;
    }
    return false;
  }

  private getTokenFromLocalStorage(): any | undefined {
    try {
      const lsValue = localStorage.getItem(this.tokenKey);
      if (!lsValue) {
        return undefined;
      }

      const authData = JSON.parse(lsValue);
      return authData;
    } catch (error) {
      console.error(error);
      return undefined;
    }
  }

  ngOnDestroy() {
    this.unsubscribe.forEach((sb) => sb.unsubscribe());
  }
}
