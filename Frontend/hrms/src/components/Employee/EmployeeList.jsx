import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './EmployeeList.css';

const EmployeeList = ({ onEditEmployee }) => {
    const [employees, setEmployees] = useState([]);

    const fetchEmployees = async () => {
        try {
            const response = await axios.get('https://localhost:7060/api/Zaposleni/getAllZaposleni');
            setEmployees(response.data);
        } catch (error) {
            console.error('Error fetching employees:', error);
        }
    };

    useEffect(() => {
        fetchEmployees();
    }, []);

    const deleteEmployee = async (employeeId) => {
        try {
            await axios.delete(`https://localhost:7060/api/Zaposleni/${employeeId}`);
            fetchEmployees();
        } catch (error) {
            console.error('Error deleting employee:', error);
        }
    };

    const handleEditClick = (employee) => {
        onEditEmployee(employee);
    };

    return (
        <div className='employee-list'>
            <table>
                <thead>
                    <tr>
                        <th>Full Name</th>
                        <th>Position</th>
                        <th>Email</th>
                        <th>Age</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {employees.length > 0 ? (
                        employees.map(employee => (
                            <tr key={employee.idZaposlenog}>
                                <td>{employee.ime} {employee.prezime}</td>
                                <td>{employee.pozicija}</td>
                                <td>{employee.email}</td>
                                <td>{employee.starost}</td>
                                <td>
                                    <button className="action-button" onClick={() => handleEditClick(employee)}>Edit</button>
                                    <button className="action-button delete" onClick={() => deleteEmployee(employee.idZaposlenog)}>Delete</button>
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="5" className="no-employees">No employees found</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
};

export default EmployeeList;
