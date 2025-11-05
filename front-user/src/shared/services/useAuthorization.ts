import { useMutation } from "@tanstack/react-query";
import type { ApiResponseDTO, AuthRequest, PermissionRequest } from "../types";

const USER_API_URL = `${import.meta.env.VITE_API_URL}/Permission/validate`;

async function ValidatePermission(request: AuthRequest<PermissionRequest>): Promise<ApiResponseDTO<boolean>> {  
        const response = await fetch(USER_API_URL, {
            method: "POST",
            headers: { 
              "Content-Type": "application/json",
              "Authorization": request.token ? `Bearer ${request.token}` : ""
            },
            credentials: 'include',
            body: JSON.stringify(request.request),
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

const useAuthorization = () => {
    return useMutation<ApiResponseDTO<boolean>, Error, AuthRequest<PermissionRequest>>({
        mutationKey: ["login"],
        mutationFn: ValidatePermission,
    });
};

export default useAuthorization;