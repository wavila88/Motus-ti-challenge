
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import './App.css';
import User from './features/userManagement/components/user';
import UserList from './features/userManagement/components/UserList';
import Login from './features/login/components/login';
import { AuthProvider } from './shared/context/AuthContext';
import SalesPersonBoard from './features/userManagement/components/SalesPerson/salesPersonBoard';
import ManagerBoard from './features/userManagement/components/ManagerBoard/managerBoard';


const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <AuthProvider>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/createUser" element={<User />} />
            <Route path="/SalesLeadBoard" element={<UserList />} />
            <Route path="/SalesPersonBoard" element={<SalesPersonBoard />} />
            <Route path="/ManagerBoard" element={<ManagerBoard />} />
            <Route path="/userEdit" element={<User />} />
          </Routes>
        </BrowserRouter>
      </AuthProvider>
    </QueryClientProvider>
  );
}

export default App;
