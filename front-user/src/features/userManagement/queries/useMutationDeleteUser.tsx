import { useMutation } from "@tanstack/react-query";
import type { ApiResponseDTO, AuthRequest } from "../../../shared/types";

const USER_API_URL = `${import.meta.env.VITE_API_URL}/User`;

async function deleteUser(req: AuthRequest<number>): Promise<ApiResponseDTO<any>> {
        
        const response = await fetch(`${USER_API_URL}/${req.request}`, {
            method: "DELETE",
            headers: { 
                "Content-Type": "application/json",
                "Authorization": req.token ? `Bearer ${req.token}` : ""
            },
            credentials: 'include',
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

const useMutationDeleteUser = () => {
    return useMutation<ApiResponseDTO<any>, Error, any>({
        mutationKey: ["deleteUser"],
        mutationFn: (req: AuthRequest<number>) => deleteUser(req),
    });
};

export default useMutationDeleteUser;