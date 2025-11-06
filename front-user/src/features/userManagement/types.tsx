export type User = {
  userId: number;
  firstName: string;
  lastName: string;
  password: string;
  email: string;
  documentNumber: string;
  dateOfBirth: string;
  roleId: string
};

export type RolDto = {
  roleId: number;
  name: string;
  level: number;
};