import { useMutation } from "@tanstack/react-query";
import type { ApiResponseDTO } from "../../../shared/types";
import type { User } from "../types";
import { useAuth } from "../../../shared/context/AuthContext";

const USER_API_URL = `${import.meta.env.VITE_API_URL}/User`;

async function saveUser(user: User): Promise<ApiResponseDTO<any>> {
        const { userInfo } = useAuth(); 
        const response = await fetch(USER_API_URL, {
            method: "POST",
            headers: { 
                "Content-Type": "application/json",
                "Authorization": userInfo ? `Bearer ${userInfo.token}` : ""
            },
            credentials: 'include',
            body: JSON.stringify(user),
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

const useMutationSaveUser = () => {
    return useMutation<ApiResponseDTO<any>, Error, any>({
        mutationKey: ["saveUser"],
        mutationFn: saveUser,
    });
};

export default useMutationSaveUser;