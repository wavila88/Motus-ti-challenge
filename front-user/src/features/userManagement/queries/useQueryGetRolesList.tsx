import { useQuery } from "@tanstack/react-query";
import type { ApiResponseDTO, AuthRequest} from "../../../shared/types";
import type { RolDto } from "../types";




// Replace with your actual API endpoint
const USER_API_URL = `${import.meta.env.VITE_API_URL}/Rol`;

const useQueryGetRolesList = (req: AuthRequest<number>) => {
    debugger;
    const getRolesList = async (): Promise<ApiResponseDTO<RolDto[]>> => {
        const response = await fetch(`${USER_API_URL}/${req.request}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": req ? `Bearer ${req.token}` : ""
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
    return useQuery<ApiResponseDTO<RolDto[]>, Error, any>({
        queryKey: ["GetRolesList"],
        queryFn: getRolesList,
    });
};

export default useQueryGetRolesList;