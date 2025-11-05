
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from "@mui/material";
import type { User } from "../types";
import useQueryGetUserList from "../queries/useQueryGetUserList";
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import useMutationDeleteUser  from "../queries/useMutationDeleteUser";
import { useNavigate } from 'react-router-dom';
import Authorize from "../../../shared/components/authorize";

const UserList = () => {
  const URL = "/SalesLeadBoard";
  const query = useQueryGetUserList();
  const mutationDelete = useMutationDeleteUser();
  const navigate = useNavigate();

  const deleteUser = async (userId: number) => {
    await mutationDelete.mutateAsync(userId);
    query.refetch();
  };

  const handleEdit = (user: User) => {
    localStorage.setItem('editUser', JSON.stringify(user));
    navigate('/userEdit'); 
  };

  return (
    <>
    <Authorize url={URL}/>
      {query.isLoading ? <h2>Loading...</h2> :
        (
          <>
            {mutationDelete.isPending && <h2>Loading...</h2>}
            <TableContainer component={Paper}>
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell>First Name</TableCell>
                    <TableCell>Last Name</TableCell>
                    <TableCell>Email</TableCell>
                    <TableCell>Position</TableCell>
                    <TableCell>Update</TableCell>
                    <TableCell>Delete</TableCell>

                  </TableRow>
                </TableHead>
                <TableBody>
                  {query.isSuccess && query.data.data?.map((user: User, idx: number) => (
                    <TableRow key={idx}>
                      <TableCell>{user.firstName}</TableCell>
                      <TableCell>{user.lastName}</TableCell>
                      <TableCell>{user.email}</TableCell>
                      <TableCell>{user.position}</TableCell>
                      <TableCell>
                        <EditIcon 
                          style={{ cursor: "pointer", color: "#1976d2" }} 
                          onClick={() => handleEdit(user)}
                        />
                      </TableCell>
                      <TableCell>
                        <DeleteIcon 
                          onClick={() =>  deleteUser(user.userId)} 
                          style={{ cursor: "pointer", color: "#d32f2f" }} />
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </>

        )


      }

    </>
  );
};

export default UserList;
