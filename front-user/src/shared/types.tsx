export interface ApiResponseDTO<T> {
  success: boolean;
  message?: string;
  data?: T;
}

export type UserInfo = {
  token: string;
  userName : string;
  rolName : string;
  levelAccess : number;
  email: string;
};

export type PermissionRequest = {
  email: string;
  permission: string;
};

export type  AuthRequest<T> = {
  token: string;
  request: T;
};
