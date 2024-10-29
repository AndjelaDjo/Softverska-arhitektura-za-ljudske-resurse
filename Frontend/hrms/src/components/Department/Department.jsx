import React, { useState, useEffect } from 'react';
import './Department.css';
import DepartmentModal from './DepartmentModal';
import DepartmentCard from './DepartmentCard'; 
import axios from 'axios';

function Department() {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [departmentName, setDepartmentName] = useState('');
    const [employeeCount, setEmployeeCount] = useState(null);
    const [selectedDepartment, setSelectedDepartment] = useState(null);

    const openModal = () => {
        setIsModalOpen(true);
    };

    const closeModal = () => {
        setIsModalOpen(false);
        setSelectedDepartment(null); 
    };

    const handleEditDepartment = (department) => {
        setSelectedDepartment(department);
        openModal();
    };

    const handleNameKeyDown = async (e) => {
        if (e.key === 'Enter') {
            try {
                const response = await axios.get(`https://localhost:7060/api/Odeljenja/getNumOfZaposlenihPoOdeljenju/${departmentName}`);
                setEmployeeCount(response.data);
            } catch (error) {
                console.error('Error fetching employee count:', error);
            }
        }
    };

    useEffect(() => {
        setEmployeeCount(null);
    }, [departmentName]);

    return (
        <div className='department-container'>
            <div className="row">
                <h1>Departments</h1>
                <button className='add-department-button' onClick={openModal}><b>Add New +</b></button>
            </div>
            <div className="input-row">
                <div className="input-group">
                    <input 
                        type="text" 
                        placeholder="Enter Department Name" 
                        value={departmentName} 
                        onChange={(e) => setDepartmentName(e.target.value)}
                        onKeyDown={handleNameKeyDown}
                    />
                    {employeeCount !== null && (
                        <p>Employee Count: {employeeCount}</p>
                    )}
                </div>
            </div>
            {isModalOpen && <DepartmentModal onRequestClose={closeModal} selectedDepartment={selectedDepartment} />}
            <DepartmentCard onEditDepartment={handleEditDepartment} />
        </div>
    );
}

export default Department;
