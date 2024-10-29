import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './DepartmentCard.css';

const DepartmentCard = ({ onEditDepartment }) => {
    const [departments, setDepartments] = useState([]);

    const fetchDepartments = async () => {
        try {
            const response = await axios.get('https://localhost:7060/api/Odeljenja/getAllOdeljenja');
            console.log('Data fetched:', response.data);
            setDepartments(response.data);
        } catch (error) {
            console.error('Error fetching departments:', error);
        }
    };

    useEffect(() => {
        fetchDepartments();
    }, []);

    const deleteDepartment = async (departmentId) => {
        try {
            await axios.delete(`https://localhost:7060/api/Odeljenja/${departmentId}`);
            console.log('Deleting department with ID:', departmentId);
            fetchDepartments();
        } catch (error) {
            console.error('Error deleting department:', error);
        }
    };

    const handleEditClick = (department) => {
        onEditDepartment(department); 
    };

    return (
        <div className='department-card-container'>
            {departments.length > 0 ? (
                departments.map(department => (
                    <div className="department-card" key={department.idOdeljenja}>
                        <p><strong>Department Name:</strong> {department.naziv}</p>
                        <p><strong>Description:</strong> {department.opis}</p>
                        <div className="department-actions">
                            <button onClick={() => handleEditClick(department)} className="edit-button">Edit</button>
                            <button onClick={() => deleteDepartment(department.idOdeljenja)} className="delete-button">Delete</button>
                        </div>
                    </div>
                ))
            ) : (
                <p>No departments found</p>
            )}
        </div>
    );
};

export default DepartmentCard;
