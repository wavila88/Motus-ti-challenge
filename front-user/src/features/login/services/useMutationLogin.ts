import { useMutation } from "@tanstack/react-query";
import type { ApiResponseDTO } from "../../../shared/types";
import type { LoginRequest } from "../types";

const USER_API_URL = `${import.meta.env.VITE_API_URL}/Permission/Login`;

async function Login(login: LoginRequest): Promise<ApiResponseDTO<any>> {
        const response = await fetch(USER_API_URL, {
            method: "POST",
            headers: { "Content-Type": "application/json"},
            credentials: 'include',
            body: JSON.stringify(login),
        });
        if (!response.ok) {
            // Try to parse error message from backend if available
            let message = "An unexpected error occurred.";
            try {
                const errorData = await response.json();
                if (errorData && errorData.message) {
                    message = errorData.message;
                }
            } catch {}
            return { success: false, message };
        }
        return response.json();
}

const useMutationLogin = () => {
    return useMutation<ApiResponseDTO<any>, Error, any>({
        mutationKey: ["login"],
        mutationFn: Login,
    });
};

export default useMutationLogin;