import { Grid, TextField, Button, MenuItem } from "@mui/material";
import { useForm } from "react-hook-form";
import { useEffect, useState } from "react";
import useMutationSaveUser from "../queries/useMutationSaveUser";
import { Link, useNavigate } from "react-router-dom";
import Authorize from "../../../shared/components/authorize";
import useQueryGetRolesList from "../queries/useQueryGetRolesList";
import { useAuth } from "../../../shared/context/AuthContext";
import type { RolDto, User } from "../types";


const User = () => {
    const URL = "/userEdit";
     const { userInfo } = useAuth();
    const navigate = useNavigate();
    const {
        register,
        handleSubmit,
        formState: { errors },
        reset
    } = useForm<User>();
    const query  = useQueryGetRolesList({
       request: userInfo?.levelAccess || 1,
       token: userInfo?.token || ""
    });
 
    // Load user data from localStorage if available
    useEffect(() => {
        const editUser = localStorage.getItem('editUser');
        if (editUser) {
            const user = JSON.parse(editUser);
            reset({
                userId: user.userId ? user.userId : 0,
                firstName: user.firstName,
                lastName: user.lastName,
                email: user.email,
                roleId: user.roleId,
                dateOfBirth: user.dateOfBirth ? user.dateOfBirth : null,
            });
            localStorage.removeItem('editUser');
        }
    }, [reset]);

    const mutation = useMutationSaveUser();

    const SaveUser = async (data: User) => {
        try {
            console.log("Date of Birth:", data);
            await mutation.mutateAsync({
              token: userInfo?.token || "",
              request:{
                userId: data.userId ? data.userId : 0,
                firstName: data.firstName,
                lastName: data.lastName,
                email: data.email,
                roleId: data.roleId,
                dateOfBirth: data.dateOfBirth,
                documentNumber: data.documentNumber,
                password: data.password
              }
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
            {/* <Authorize url={URL} /> */}
            <nav style={{ margin: 16 }}>
                <Link to="/" style={{ marginRight: 16 }}>Create User</Link>
                <Link to="/userEdit">User List</Link>
            </nav>
            {mutation.isPending && <h2>Loading ...</h2>}
            <form onSubmit={handleSubmit(SaveUser)}>
                <input type="hidden" {...register("userId")}/>
                <Grid container spacing={2}>
                    <Grid size={{ xs: 12 }}>
                        <TextField 
                            {...register("firstName", {
                                required: "First Name is required",
                                minLength: {
                                    value: 4,
                                    message: "min length is 4"
                                }
                            })}
                            error={!!errors["firstName"]}
                            className="input"
                            id="outlined-basic"
                            label="First Name"
                            variant="outlined"
                            helperText={typeof errors["firstName"]?.message === "string" ? errors["firstName"]?.message : ""}
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
                            {...register("documentNumber", {
                                required: "Document Number is required",
                                minLength: {
                                    value: 4,
                                    message: "min length is 4"
                                },
                              
                                 
                            })}
                            error={!!errors["documentNumber"]}
                            className="input"
                            id="outlined-basic"
                            label="Document Number"
                            variant="outlined"
                            helperText={typeof errors["documentNumber"]?.message === "string" ? errors["documentNumber"]?.message : ""}
                        />
                    </Grid>
                    <Grid size={{ xs: 12 }}>
                        <TextField
                            {...register("email", {
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
                            error={!!errors["email"]}
                            className="input"
                            id="outlined-basic"
                            label="Email"
                            variant="outlined"
                            helperText={typeof errors["email"]?.message === "string" ? errors["email"]?.message : ""}
                        />
                    </Grid>
                        <Grid size={{ xs: 12 }}>
                        <TextField
                            {...register("password", {
                                required: "Password is required",
                                minLength: {
                                    value: 4,
                                    message: "min length is 4"
                                },
                                //Validations for password format
                                 pattern: {
                                    value: /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{4,}$/,
                                    message: "Invalid password format"
                                }
                            })}
                            error={!!errors["password"]}
                            className="input"
                            id="outlined-basic"
                            type="password"
                            label="Password"
                            variant="outlined"
                            helperText={typeof errors["password"]?.message === "string" ? errors["password"]?.message : ""}
                        />
                    </Grid>
                    <Grid size={4}>
                        <TextField
                            fullWidth
                            {...register("dateOfBirth", { required: "Date of Birth is required" })}
                            error={!!errors["dateOfBirth"]}
                            className="input"
                            id="date-of-birth"
                            label="Date of Birth"
                            type="date"
                            InputLabelProps={{ shrink: true }}
                            variant="outlined"
                            helperText={typeof errors["dateOfBirth"]?.message === "string" ? errors["dateOfBirth"]?.message : ""}
                        />
                    </Grid>
                    <Grid size={12} />
                    <Grid size={{ xs: 4 }}>
                        <TextField
                            select
                            fullWidth
                            {...register("roleId", {
                                required: "Role is required",
                                minLength: {
                                    value: 4,
                                    message: "min length is 4"
                                }
                            })}
                            error={!!errors["roleId"]}
                            className="input"
                            id="outlined-basic"
                            label="Role"
                            variant="outlined"
                            helperText={typeof errors["roleId"]?.message === "string" ? errors["roleId"]?.message : ""}
                        >
                            {query.data && query.data.data && Array.isArray(query.data.data) && query.data.data.map((role: RolDto) => (
                                <MenuItem key={role.roleId} value={role.roleId}>
                                    {role.name}
                                </MenuItem>
                            ))}
                        </TextField>
                    </Grid>
                    <Grid size={12} />


                    <Button type="submit" variant="contained">Submit</Button>

                </Grid>
            </form>
        </>
    )
}

export default User;
