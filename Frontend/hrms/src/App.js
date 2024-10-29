import './App.css';
import { BrowserRouter as Router, Route, Routes, Navigate, useLocation } from 'react-router-dom';
import LoginForm from './components/forms/LoginForm/LoginForm';
import RegisterForm from './components/forms/RegisterForm/RegisterForm';
import Menu from './components/Menu/Menu.jsx';
import Employee from './components/Employee/Employee.jsx';
import Leave from './components/Leave/Leave.jsx';
import Department from './components/Department/Department.jsx';
import Dashboard from './components/Dashboard/Dashboard.jsx';

function App() {
  return (
    <Router>
      <Main />
    </Router>
  );
}

function Main() {
  const location = useLocation();
  const showMenu = location.pathname === "/employee" || location.pathname === "/leave" || location.pathname === "/department"
  || location.pathname === "/dashboard";

  return (
    <div className="App">
      {showMenu && <Menu />}
      <Routes>
        <Route path="/" element={<Navigate to="/login" />} />
        <Route path="/login" element={<LoginForm />} />
        <Route path="/register" element={<RegisterForm />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/employee" element={<Employee />} />
        <Route path="/leave" element={<Leave />} />
        <Route path="/department" element={<Department />} />
      </Routes>
    </div>
  );
}

export default App;
