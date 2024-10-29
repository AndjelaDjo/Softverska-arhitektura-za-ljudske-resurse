import React, { useState } from 'react';
import './Leave.css';
import LeaveList from './LeaveList';
import axios from 'axios';

const Leave = ({ onRequestClose }) => {
    const [leave, setLeave] = useState({
        datumPocetka: '',
        datumZavrsetka: '',
        Razlog: '',
        idZaposlenog: ''
    });
    const [editingLeave, setEditingLeave] = useState(null);
    const [filterStartDate, setFilterStartDate] = useState('');
    const [filterEmployeeId, setFilterEmployeeId] = useState('');
    const [checkResult, setCheckResult] = useState('');
    const [errorMessage, setErrorMessage] = useState(''); // New state for error messages

    const handleChange = (e) => {
        const { name, value } = e.target;
        setLeave(prevState => ({
            ...prevState,
            [name]: value
        }));
        setErrorMessage(''); // Clear error message on input change
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (editingLeave) {
                const response = await axios.put(`https://localhost:7060/api/Odsustva/${editingLeave.idOdsustva}`, leave);
                console.log('Leave updated:', response.data);
            } else {
                const response = await axios.post('https://localhost:7060/api/Odsustva', leave);
                console.log('Leave saved:', response.data);
            }

            setLeave({
                datumPocetka: '',
                datumZavrsetka: '',
                Razlog: '',
                idZaposlenog: ''
            });
            setEditingLeave(null);
            onRequestClose();
        } catch (error) {
            console.error('Error saving leave:', error);
        }
    };

    const handleCheckLeaveAllowed = async () => {
        const { idZaposlenog, datumPocetka, datumZavrsetka } = leave;

        if (!idZaposlenog || !datumPocetka) {
            setErrorMessage('Please enter Employee ID and Start Date to check leave availability.');
            return;
        }

        if (datumZavrsetka && new Date(datumZavrsetka) <= new Date(datumPocetka)) {
            setErrorMessage('End Date must be after Start Date.'); // Set error message
            return;
        }

        try {
            const url = `https://localhost:7060/api/Odsustva/checkIfOdsustvoAllowed/${idZaposlenog}/${datumPocetka}` + 
                        (datumZavrsetka ? `/${datumZavrsetka}` : '');
            const response = await axios.get(url);
            setCheckResult(response.data);
            setErrorMessage(''); 
        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.error('Error checking leave allowance:', error.response?.data || error.message);
                setCheckResult('Error checking leave allowance: ' + (error.response?.data.message || error.message));
            } else {
                console.error('Unexpected error:', error);
                setCheckResult('Unexpected error. Please try again later.');
            }
        }
    };

    const handleKeyDown = (e) => {
        if (e.key === 'Enter') {
            if (e.target.name === 'datumPocetka') {
                setFilterStartDate(leave.datumPocetka);
            } else if (e.target.name === 'idZaposlenog') {
                setFilterEmployeeId(leave.idZaposlenog);
            }
        }
    };

    const formatDateForInput = (dateString) => {
        const date = new Date(dateString);
        
        if (isNaN(date.getTime())) {
            console.error('Invalid date:', dateString);
            return '';
        }

        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0'); 
        const day = String(date.getDate()).padStart(2, '0');
    
        return `${year}-${month}-${day}`;
    };
    
    const startEditing = (leaveToEdit) => {
        setLeave({
            datumPocetka: formatDateForInput(leaveToEdit.datumPocetka),
            datumZavrsetka: formatDateForInput(leaveToEdit.datumZavrsetka),
            Razlog: leaveToEdit.razlog,
            idZaposlenog: leaveToEdit.idZaposlenog
        });
        setEditingLeave(leaveToEdit);
        setErrorMessage('');
    };
    

    return (
        <div className='leave-container'>
            <h1>Leave</h1>
            <form onSubmit={handleSubmit}>
                <div className="row">
                    <div className="input-leave">
                        <p>Employee</p>
                        <input 
                            type="text" 
                            name="idZaposlenog" 
                            placeholder='Employee ID' 
                            value={leave.idZaposlenog} 
                            onChange={handleChange} 
                            onKeyDown={handleKeyDown}
                        />
                    </div>
                    <div className="input-leave">
                        <p>Start Date</p>
                        <input 
                            type="date" 
                            name="datumPocetka" 
                            placeholder='Start Date' 
                            value={leave.datumPocetka} 
                            onChange={handleChange} 
                            onKeyDown={handleKeyDown}
                        />
                    </div>
                    <div className="input-leave">
                        <p>End Date</p>
                        <input 
                            type="date" 
                            name="datumZavrsetka" 
                            placeholder='End Date' 
                            value={leave.datumZavrsetka} 
                            onChange={handleChange} 
                        />
                    </div>
                    <div className="input-leave">
                        <p>Reason</p>
                        <input 
                            type="text" 
                            name="Razlog" 
                            placeholder='Reason' 
                            value={leave.Razlog} 
                            onChange={handleChange} 
                        />
                    </div>
                    <div className="button-group">
                        <button type="submit" className="save-leave-button">
                            {editingLeave ? 'Update' : 'Save'}
                        </button>
                        <button type="button" onClick={handleCheckLeaveAllowed} className="check-leave-button">
                            Check Leave
                        </button>
                    </div>
                </div>
            </form>
            {errorMessage && <p className="error-message">{errorMessage}</p>}
            {checkResult && <p>{checkResult}</p>}
            <LeaveList 
                filterStartDate={filterStartDate} 
                filterEmployeeId={filterEmployeeId} 
                onEdit={startEditing} 
            />
        </div>
    );
}

export default Leave;
