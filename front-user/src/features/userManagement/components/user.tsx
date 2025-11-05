import { Grid, TextField, Button } from "@mui/material";
import { useForm } from "react-hook-form";
import { useEffect } from "react";
import useMutationSaveUser from "../queries/useMutationSaveUser";
import { Link, useNavigate } from "react-router-dom";
import Authorize from "../../../shared/components/authorize";

const User = () => {
    const URL = "/userEdit";
    const navigate = useNavigate();
    const {
        register,
        handleSubmit,
        formState: { errors },
        reset
    } = useForm();
    // Load user data from localStorage if available
    useEffect(() => {
        const editUser = localStorage.getItem('editUser');
        if (editUser) {
            const user = JSON.parse(editUser);
            reset({
                userId: user.userId ? user.userId : 0,
                FirstName: user.firstName,
                lastName: user.lastName,
                Email: user.email,
                Position: user.position,
            });
            localStorage.removeItem('editUser');
        }
    }, [reset]);

    const mutation = useMutationSaveUser();

    const SaveUser = async (data: any) => {
        try {
            await mutation.mutateAsync({
                userId: data.userId ? data.userId : 0,
                firstName: data.FirstName,
                lastName: data.lastName,
                email: data.Email,
                position: data.Position,
            });
            mutation.isSuccess && alert("User saved successfully");
            reset();
            mutation.reset();
            navigate('/SalesLeadBoard'); // Navigate to user list after saving
        } catch {
            alert("Error saving user");
        }
    }

    return (
        <>
            <Authorize url={URL} />
            <nav style={{ margin: 16 }}>
                <Link to="/" style={{ marginRight: 16 }}>Create User</Link>
                <Link to="/list">User List</Link>
            </nav>
            {mutation.isPending && <h2>Loading ...</h2>}
            <form onSubmit={handleSubmit(SaveUser)}>
                <input type="hidden" {...register("userId")}/>
                <Grid container spacing={2}>
                    <Grid size={{ xs: 12 }}>
                        <TextField
                            {...register("FirstName", {
                                required: "First Name is required",
                                minLength: {
                                    value: 4,
                                    message: "min length is 4"
                                }
                            })}
                            error={!!errors["FirstName"]}
                            className="input"
                            id="outlined-basic"
                            label="First Name"
                            variant="outlined"
                            helperText={typeof errors["FirstName"]?.message === "string" ? errors["FirstName"]?.message : ""}
                        />
                    </Grid>
                    <Grid size={{ xs: 12 }}>
                        <TextField
                            {...register("lastName", {
                                required: "Last Name is required",
                                minLength: {
                                    value: 4,
                                    message: "min length is 4"
                                }
                            })}
                            error={!!errors["lastName"]}
                            className="input"
                            id="outlined-basic"
                            label="lastName"
                            variant="outlined"
                            helperText={typeof errors["lastName"]?.message === "string" ? errors["lastName"]?.message : ""}
                        />
                    </Grid>
                    <Grid size={{ xs: 12 }}>
                        <TextField
                            {...register("Email", {
                                required: "Email is required",
                                minLength: {
                                    value: 4,
                                    message: "min length is 4"
                                },
                                //Validations for email format
                                 pattern: {
                                    value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                                    message: "Invalid email address"
                                }
                            })}
                            error={!!errors["Email"]}
                            className="input"
                            id="outlined-basic"
                            label="Email"
                            variant="outlined"
                            helperText={typeof errors["Email"]?.message === "string" ? errors["Email"]?.message : ""}
                        />
                    </Grid>
                    <Grid size={{ xs: 12 }}>
                        <TextField
                            {...register("Position", {
                                required: "Position is required",
                                minLength: {
                                    value: 4,
                                    message: "min length is 4"
                                }
                            })}
                            error={!!errors["Position"]}
                            className="input"
                            id="outlined-basic"
                            label="Position"
                            variant="outlined"
                            helperText={typeof errors["Position"]?.message === "string" ? errors["Position"]?.message : ""}
                        />
                    </Grid>


                    <Button type="submit" variant="contained">Submit</Button>

                </Grid>
            </form>
        </>
    )
}

export default User;
