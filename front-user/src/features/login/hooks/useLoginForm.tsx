import { useForm } from "react-hook-form";
import useMutationLogin from "../services/useMutationLogin";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../../shared/context/AuthContext";

const useLoginForm = () => {
    const mutation = useMutationLogin();
    const navigate = useNavigate();
    const { setUserInfo } = useAuth();
    const {
        register,       
        handleSubmit,
        formState: { errors },
        reset
    } = useForm();

    const redirectByLevel = (level: number) => {
        if (level === 1) {
             navigate("/SalesPersonBoard");
        } else if (level === 2) {
            navigate("/SalesLeadBoard");
        } else if (level === 3) {
              navigate("/ManagerBoard");
        } else {
            navigate("/SalesPersonBoard");
        }
    };

    const LoginMethod = async (data: any) => {
        // Simulate login process
        try {
            debugger;
            const result = await mutation.mutateAsync({
                email: data.email,
                password: data.password,
            });
            debugger;
            if (result && result.data) {
                setUserInfo(result.data);
                redirectByLevel(result.data.levelAccess);
                reset();
                mutation.reset();
            }
        } catch {
            alert("Login failed");
        }
    }
    const emailValidation = {
        required: "Email is required",
        minLength: {
            value: 8,
            message: "Min length is 8"
        },
        pattern: {
            value: /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/,
            message: "Invalid email format"
        }
    };

    const passwordValidation = {
        required: "Password is required",
        minLength: {
            value: 8,
            message: "Min length is 8"
        },
        // pattern: {
        //     value: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$/,
        //     message: "Password must contain uppercase, lowercase, number, and special character"
        // }
    };

    return {
        register,
        handleSubmit,
        errors,
        reset,
        LoginMethod,
        emailValidation,
        passwordValidation
    };
};

export default useLoginForm;
