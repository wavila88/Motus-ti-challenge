import Authorize from "../../../../shared/components/authorize";
import BoardNavigation from "../../../../shared/components/BoardNavigation";
import { useAuth } from "../../../../shared/context/AuthContext";

const SalesPersonBoard = () => {
    const URL = "/SalesPersonBoard";
    const { userInfo } = useAuth();
    return (
        <div>
            <Authorize url={URL} />
            <h1>Sales Person Board</h1>
            <h2>Welcome, {userInfo?.userName || "User"}!</h2>
            <p>You are authorized to access this page.</p>
            {/* Add your sales person board content here */}
            <BoardNavigation />
        </div>
    );
};

export default SalesPersonBoard;
