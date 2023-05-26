import React, { useState, useEffect } from 'react';
import { CSVLink } from 'react-csv'; //npm i react-csv
import "./ExportCSV.css";  // see https://surajsharma.net/blog/react-create-csv-file

const ExportCSV = () => {
    // fetched data must be [] or string to use {CSVLink}
    const [fileData, setFileData] = useState([]); //initialize to empty []

    const [headers] = useState([ // from experimenting, label is table label. key is actual key of DB (case sensitive)
        { label: 'Id', key: 'Id' }, 
        { label: 'StartDis', key: 'StartDis' },
        { label: 'EndDis', key: 'EndDis' },
        { label: 'string1', key: 'string1' },
        { label: 'string2', key: 'string2' },
    ]);

    const handleDataFetch = async () => {
        const response = await fetch("https://localhost:44365/api/Book/GetAllBooks");
        const respJSON = await response.json();
        setFileData(respJSON); //update [fileData] with DB data
    };

    useEffect(() => { //how does this work?
        handleDataFetch();
    }, []);

    return (
        <div>
            <h3>Export to CSV</h3>
            <h2>Reference link</h2>
            
            {fileData?.length &&
                <CSVLink
                    headers={headers}
                    data={fileData}
                    filename="results.csv"
                    target="_blank" //not sure what this does
                >
                <button>Export</button>
                </CSVLink>
                
            }
        </div >
    );
}
export default ExportCSV; // call <ExportCSV /> in App.js
