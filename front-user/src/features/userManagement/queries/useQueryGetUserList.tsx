import { useQuery } from "@tanstack/react-query";
import type { ApiResponseDTO} from "../../../shared/types";
import type { User } from "../types";
import { useAuth } from "../../../shared/context/AuthContext";



// Replace with your actual API endpoint
const USER_API_URL = `${import.meta.env.VITE_API_URL}/User`;

const useQueryGetUserList = () => {
    debugger;
    const { userInfo } = useAuth();        
    const getUserList = async (): Promise<ApiResponseDTO<User[]>> => {
        const response = await fetch(`${USER_API_URL}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": userInfo ? `Bearer ${userInfo.token}` : ""
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
    };
    return useQuery<ApiResponseDTO<User[]>, Error, any>({
        queryKey: ["getUserList"],
        queryFn: getUserList,
    });
};

export default useQueryGetUserList;