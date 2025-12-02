import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
    providedIn: 'root'
})
export class AlertService {

    constructor() { }

    showSuccessMessage(message: string): void {
        Swal.fire({
            title: 'Success!',
            text: message,
            icon: 'success',
            confirmButtonText: 'Đồng ý',
            customClass: {
                confirmButton: 'btn btn-sm btn-success', // Custom class for the confirm button
                cancelButton: 'btn btn-sm btn-danger'    // Custom class for the cancel button
            },
            allowOutsideClick: false, // Prevent closing the dialog by clicking outside
        });
    }

    // Confirmation message
    showConfirmationDialog(message: string): Promise<boolean> {
        return Swal.fire({
            title: 'Bạn có đồng ý không?',
            text: message,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Đồng ý',
            cancelButtonText: 'Không',
            customClass: {
                confirmButton: 'btn btn-sm btn-primary', // Custom class for the confirm button
                cancelButton: 'btn btn-sm btn-danger'    // Custom class for the cancel button
            },
            allowOutsideClick: false, // Prevent closing the dialog by clicking outside
            allowEscapeKey: true,    // Prevent closing the dialog by pressing ESC key
            buttonsStyling: false // Disables the default button styles so custom styles work
        }).then((result) => {
            return result.isConfirmed; // Returns true if the user confirms, false if they cancel
        });
    }

    showErrorMessage(message: string): void {
        Swal.fire({
            title: 'Error!',
            text: message,
            icon: 'error',
            confirmButtonText: 'Đóng',
            customClass: {
                confirmButton: 'btn btn-sm btn-danger', // Custom class for the confirm button
                cancelButton: 'btn btn-sm btn-danger'    // Custom class for the cancel button
            },
            allowOutsideClick: false, // Prevent closing the dialog by clicking outside
        });
    }

    showInfoMessage(message: string): void {
        Swal.fire({
            title: 'Information',
            text: message,
            icon: 'info',
            confirmButtonText: 'Đóng',
            customClass: {
                confirmButton: 'btn btn-sm btn-info', // Custom class for the confirm button
            },
            allowOutsideClick: false, // Prevent closing the dialog by clicking outside
        });
    }

    showWarningMessage(message: string): void {
        Swal.fire({
            title: 'Warning!',
            text: message,
            icon: 'warning',
            confirmButtonText: 'OK',
            customClass: {
                confirmButton: 'btn btn-sm btn-warning', // Custom class for the confirm button
            },
            allowOutsideClick: false, // Prevent closing the dialog by clicking outside
        });
    }
}
