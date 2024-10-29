import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './EmployeeModal.css';

const EmployeeModal = ({ onRequestClose, selectedEmployee }) => {
    const [employee, setEmployee] = useState({
        Ime: '',
        Prezime: '',
        Email: '',
        Pozicija: '',
        Pol: '',
        Starost: 0,
        idOdeljenja: 0
    });

    useEffect(() => {
        if (selectedEmployee) {
            setEmployee({
                Ime: selectedEmployee.ime || '',
                Prezime: selectedEmployee.prezime || '',
                Email: selectedEmployee.email || '',
                Pozicija: selectedEmployee.pozicija || '',
                Pol: selectedEmployee.pol || '',
                Starost: selectedEmployee.starost || 0,
                idOdeljenja: selectedEmployee.idOdeljenja || 0
            });
        }
    }, [selectedEmployee]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setEmployee(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (selectedEmployee) {
                await axios.put(`https://localhost:7060/api/Zaposleni/${employee.idZaposlenog}`, employee);
                console.log('Employee updated:', employee);
            } else {
                await axios.post('https://localhost:7060/api/Zaposleni', employee);
                console.log('New employee added:', employee);
            }
            onRequestClose();
        } catch (error) {
            console.error('Error saving employee:', error);
        }
    };

    return (
        <div className="modal-overlay" onClick={onRequestClose}>
            <div className="modal-content" onClick={(e) => e.stopPropagation()} style={{ width: '500px', height: '530px' }}>
                <h2>{selectedEmployee ? 'Edit Employee' : 'Add New Employee'}</h2>
                <form onSubmit={handleSubmit} className="employee-form">
                    <div className="input-data">
                        <input type="text" name="Ime" placeholder="First Name" value={employee.Ime} onChange={handleChange} required />
                    </div>
                    <div className="input-data">
                        <input type="text" name="Prezime" placeholder="Last Name" value={employee.Prezime} onChange={handleChange} required />
                    </div>
                    <div className="input-data">
                        <input type="email" name="Email" placeholder="Email" value={employee.Email} onChange={handleChange} required />
                    </div>
                    <div className="input-data">
                        <input type="text" name="Pozicija" placeholder="Position" value={employee.Pozicija} onChange={handleChange} required />
                    </div>
                    <div className="input-data">
                        <input type="text" name="Pol" placeholder="Gender" value={employee.Pol} onChange={handleChange} required />
                    </div>
                    <div className="input-data">
                        <input type="number" name="Starost" placeholder="Age" value={employee.Starost} onChange={handleChange} required />
                    </div>
                    <div className="input-data">
                        <input type="number" name="idOdeljenja" placeholder="Department ID" value={employee.idOdeljenja} onChange={handleChange} required />
                    </div>
                    <div className="input-data">
                        <button type="submit" className="save-employee-button">
                            {selectedEmployee ? 'Update' : 'Save'}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default EmployeeModal;
