import { Button, Stack } from "@mui/material";
import { useNavigate } from "react-router-dom";

const BoardNavigation = () => {
  const navigate = useNavigate();
  return (
    <Stack direction="row" spacing={2} sx={{ my: 2 }}>
      <Button variant="contained" onClick={() => navigate("/SalesLeadBoard")}>Sales Lead Board</Button>
      <Button variant="contained" onClick={() => navigate("/userEdit")}>Create User</Button>
      <Button variant="contained" onClick={() => navigate("/SalesPersonBoard")}>Sales Person Board</Button>
      <Button variant="contained" onClick={() => navigate("/ManagerBoard")}>Manager Board</Button>
    </Stack>
  );
};

export default BoardNavigation;
