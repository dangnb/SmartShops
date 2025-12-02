export interface ApiResponse<T> {
    value: T;  // Mảng dữ liệu trả về
    isSuccess: boolean;  // Trạng thái thành công
    isFailure: boolean;  // Trạng thái thất bại
    error: ErrorInfo;  // Thông tin lỗi
}

export interface ErrorInfo {
    code: string;
    message: string;
}