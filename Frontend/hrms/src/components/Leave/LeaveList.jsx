import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './LeaveList.css';

function LeaveList({ filterStartDate, filterEmployeeId, onEdit }) {
    const [leaves, setLeaves] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let url = 'https://localhost:7060/api/Odsustva/getAllOdsustva';
                if (filterStartDate) {
                    url = `https://localhost:7060/api/Odsustva/getByDatumPocetka/${filterStartDate}`;
                } else if (filterEmployeeId) {
                    url = `https://localhost:7060/api/Odsustva/getByZaposleniId/${filterEmployeeId}`;
                }

                const response = await axios.get(url);
                const data = response.data;

                const leavePromises = data.map(async (leave) => {
                    if (leave.idZaposlenog) {
                        const employeeResponse = await axios.get(`https://localhost:7060/api/Zaposleni/getNameSurnameById/${leave.idZaposlenog}`);
                        const employee = employeeResponse.data;
                        return { ...leave, employeeName: `${employee.ime} ${employee.prezime}` };
                    } else {
                        return leave;
                    }
                });

                const resolvedLeaves = await Promise.all(leavePromises);
                setLeaves(resolvedLeaves);
            } catch (error) {
                console.error('Error fetching data:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [filterStartDate, filterEmployeeId]);

    const deleteLeave = async (leave) => {
        if (!leave) return;
        try {
            await axios.delete(`https://localhost:7060/api/Odsustva/${leave.idOdsustva}`);
            setLeaves(leaves.filter(l => l.idOdsustva !== leave.idOdsustva));
        } catch (error) {
            console.error('Error deleting leave:', error);
        }
    };

    const editLeave = (leave) => {
        onEdit(leave);
    };

    const formatDate = (dateString) => {
        const options = { day: '2-digit', month: '2-digit', year: 'numeric' };
        return new Date(dateString).toLocaleDateString('en-GB', options);
    };

    return (
        <div className='leave-list'>
            <table>
                <thead>
                    <tr>
                        <th>Employee Full Name</th>
                        <th>Start Date</th>
                        <th>End Date</th>
                        <th>Reason</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {loading ? (
                        <tr>
                            <td colSpan="5">Loading...</td>
                        </tr>
                    ) : leaves.length > 0 ? (
                        leaves.map(leave => (
                            <tr key={leave.idOdsustva}>
                                <td>{leave.employeeName || 'Unknown'}</td>
                                <td>{formatDate(leave.datumPocetka)}</td>
                                <td>{formatDate(leave.datumZavrsetka)}</td>
                                <td>{leave.razlog}</td>
                                <td>
                                    <div className="action-buttons">
                                        <button className="action-button" onClick={() => editLeave(leave)}>Edit</button>
                                        <button className="action-button delete" onClick={() => deleteLeave(leave)}>Delete</button>
                                    </div>
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="5">No leaves found.</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
}

export default LeaveList;
