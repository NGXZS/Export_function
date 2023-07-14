import React, { useState } from 'react';
import { CSVLink } from "react-csv";
import "./ExportCSV.css";  // see https://surajsharma.net/blog/react-create-csv-file
import DataTable from 'react-data-table-component';

const ExportCSV = () => {
    const headers = [ // key must follow key of data, label is wtv you want
        { label: 'Id', key: 'id' }, 
        { label: 'StartDis', key: 'startDis' },
        { label: 'EndDis', key: 'endDis' },
        { label: 'string1', key: 'string1' },
        { label: 'string2', key: 'string2' }
    ];

    // fetched data must be [] or string to use {CSVLink}
    const [fileData, setFileData] = useState([]); //initialize to empty []
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(1);
    const [CSVReport, setCSVReport] = useState({
        filename: 'report.csv',
        headers: headers,
        data: fileData
    });

    const handleDataFetch = () => { 
        return fetch(`https://localhost:44365/api/Book?PageNumber=${pageNumber}&PageSize=${pageSize}`)
        .then(response => response.json())
        .then((json) => {
            const arr = [];
            json.data.map(elem => arr.push(elem));
            // console.log("the arr is :");
            // console.log(arr);
            setFileData(arr);
            console.log("pageSize is:");
            console.log(pageSize);
            //handleCSVReport();
        });
    }  
    
    const handleCSVReport = () => {
        setCSVReport({
            ...CSVReport, //copies old fields
            data: fileData});
            console.log("fileData is : ");
            console.log(fileData);
    }
    
    return (
        <div>
            <h3>Export to CSV</h3>
            <h2>Export Feature </h2>
            <label>Page Size: </label>
            <input placeholder="pageSize" value={pageSize} onChange={(e) => setPageSize(e.target.value)} />
            <label>Page Number: </label>
            <input placeholder="pageNumber" value={pageNumber} onChange={(e) => setPageNumber(e.target.value)} />   
            <button onClick={handleDataFetch}>Show</button>
            <CSVLink {...CSVReport}> //takes in wtv is in CSVReport
            <button onClick={handleCSVReport}>Export</button>
            </CSVLink> 
            <div> //shows data we are selecting
                <table>
                    <thead>
                        <tr>
                            {headers.map((object) => {
                                return(
                                    <th>{object.label}</th>
                                )
                            })}
                        </tr>
                    </thead>
                    <tbody>
                        {fileData.map((row) =>{
                            return(
                                <tr>
                                    <td>{row.id}</td>
                                    <td>{row.startDis}</td>
                                    <td>{row.endDis}</td>
                                    <td>{row.string1}</td>
                                    <td>{row.string2}</td>
                                </tr>
                            )
                        })}
                    </tbody>
                </table>
                </div>     
        </div >
    );
}

export default ExportCSV; // call <ExportCSV /> in App.js*
