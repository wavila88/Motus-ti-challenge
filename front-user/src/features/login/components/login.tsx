import { TextField, Button, Box, Typography, Paper } from "@mui/material";
import useLoginForm from "../hooks/useLoginForm";

const Login = () => {
    const {
        register,
        handleSubmit,
        errors,
        LoginMethod,
        emailValidation,
        passwordValidation
    } = useLoginForm();

    return (
        <Box display="flex" justifyContent="center" alignItems="center" minHeight="70vh">
            <Paper elevation={3} sx={{ padding: 4, width: 350 }}>
                <Typography variant="h5" align="center" gutterBottom>
                    Sign In
                </Typography>
                <form onSubmit={handleSubmit(LoginMethod)}>
                    <TextField              
                        {...register("email", emailValidation)}
                        label="Email"
                        variant="outlined"
                        fullWidth
                        margin="normal"
                        error={!!errors.email}
                        helperText={typeof errors["email"]?.message === "string" ? errors["email"]?.message : ""}
                    />
                    <TextField
                        {...register("password", passwordValidation)}
                        label="Password"
                        type="password"
                        variant="outlined"
                        fullWidth
                        margin="normal"
                        error={!!errors.password}
                        helperText={typeof errors["password"]?.message === "string" ? errors["password"]?.message : ""}
                    />
                    <Button type="submit" variant="contained" color="primary" fullWidth sx={{ mt: 2 }}>
                        Login
                    </Button>
                </form>
            </Paper>
        </Box>
    );
};

export default Login;