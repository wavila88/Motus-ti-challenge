import { memo, useEffect } from "react";
import { useAuth } from "../context/AuthContext";
import useAuthorization from "../services/useAuthorization";
import { useNavigate } from "react-router-dom";
interface AuthorizeProps {
    url: string;
}
const Authorize = (props: AuthorizeProps) => {
    const { userInfo } = useAuth();
    const mutation = useAuthorization();
    const navigate = useNavigate();

    useEffect(() => {
        if (!userInfo) {
            navigate("/");
            return;
        }
        mutation.mutateAsync({
            token: userInfo?.token || "",
            request: {
                permission: props.url,
                email: userInfo?.email || "",
            },
        }).then((res) => {
            debugger;
            if (!res.data) {
                navigate("/");
            }
        });
    }, []);

    return (
        <></>
    );
};

export default memo(Authorize);
