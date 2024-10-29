import React, { useState, useEffect } from 'react';
import './DepartmentModal.css';
import axios from 'axios';

const DepartmentModal = ({ onRequestClose, selectedDepartment }) => {
    const [department, setDepartment] = useState({
        Naziv: '',
        Opis: '',
        idOdeljenja: null 
    });

    useEffect(() => {
        if (selectedDepartment) {
            setDepartment({
                Naziv: selectedDepartment.naziv || '', 
                Opis: selectedDepartment.opis || '',
                idOdeljenja: selectedDepartment.idOdeljenja || null
            });
        } else {
            setDepartment({
                Naziv: '',
                Opis: '',
                idOdeljenja: null 
            });
        }
    }, [selectedDepartment]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setDepartment(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (department.idOdeljenja) {
                await axios.put(`https://localhost:7060/api/Odeljenja/${department.idOdeljenja}`, department);
                console.log('Department updated:', department);
            } else {
                await axios.post('https://localhost:7060/api/Odeljenja', department);
                console.log('Department saved:', department);
            }
            onRequestClose(); 
        } catch (error) {
            console.error('Error saving department:', error);
        }
    };

    return (
        <div className="modal-overlay" onClick={onRequestClose}>
            <div className="modal-content" onClick={(e) => e.stopPropagation()} style={{ width: '500px', height: '300px' }}>
                <h2>{selectedDepartment ? 'Edit Department' : 'Add New Department'}</h2>
                <form onSubmit={handleSubmit} className="department-form">
                    <div className="input-department">
                        <input 
                            type="text" 
                            name="Naziv" 
                            placeholder="Name" 
                            value={department.Naziv} 
                            onChange={handleChange} 
                            required 
                        />
                    </div>
                    <div className="input-department">
                        <textarea 
                            name="Opis" 
                            placeholder="Description" 
                            rows={10} 
                            value={department.Opis} 
                            onChange={handleChange} 
                            required
                        ></textarea>
                    </div>
                    <div className="input-data">
                        <button type="submit" className="save-department-button">
                            {selectedDepartment ? 'Update' : 'Save'}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default DepartmentModal;
