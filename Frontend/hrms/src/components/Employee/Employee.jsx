import React, { useState } from 'react';
import './Employee.css';
import EmployeeModal from './EmployeeModal';
import EmployeeList from './EmployeeList';

function Employee() {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedEmployee, setSelectedEmployee] = useState(null);

    const openModal = () => {
        setIsModalOpen(true);
    };

    const closeModal = () => {
        setIsModalOpen(false);
        setSelectedEmployee(null); 
    };

    const handleEditEmployee = (employee) => {
        setSelectedEmployee(employee);
        openModal();
    };

    return (
        <div className='employee-container'>
            <div className="row">
                <h1>Employees</h1>
                <button className='add-employee-button' onClick={openModal}><b>Add New +</b></button>
            </div>
            {isModalOpen && <EmployeeModal onRequestClose={closeModal} selectedEmployee={selectedEmployee} />}
            <EmployeeList onEditEmployee={handleEditEmployee} />
        </div>
    );
}

export default Employee;
