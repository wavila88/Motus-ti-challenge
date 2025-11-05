import { createContext, useContext, useState,type ReactNode } from "react";
import type { UserInfo } from "../types";

interface AuthContextType {

  userInfo: UserInfo | null;
  setUserInfo: (userInfo: UserInfo | null) => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {

  const [userInfo, setUserInfo] = useState<UserInfo | null>(null);

  return (
    <AuthContext.Provider value={{ userInfo , setUserInfo }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
