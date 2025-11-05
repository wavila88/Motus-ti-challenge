import Authorize from "../../../../shared/components/authorize";
import { useAuth } from "../../../../shared/context/AuthContext";


const ManagerBoard = () => {
    const { userInfo } = useAuth();
    const URL = "/ManagerBoard";
    debugger;

    return (
        <div>
            <Authorize url={URL} />
            <h1>Manager Board</h1>
            <h2>Welcome, {userInfo?.userName || "User"}!</h2>
            <p>You are authorized to access this page.</p>
            {/* Add your manager board content here */}
        </div>
    );
};

export default ManagerBoard;
