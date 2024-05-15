import { useState } from 'react';
import './App.css';
import Workers from './components/Workers';
import Divisions from './components/Divisions';

const App = ({ }) => {
    const [selectedDivisionId, setSelectedDivisionId] = useState<number | undefined>(undefined);

    return (
        <div className='content'>
            <Divisions
                selectedDivisionId={selectedDivisionId}
                setSelectedDivisionId={setSelectedDivisionId}
            />
            <Workers
                selectedDivisionId={selectedDivisionId}
            />
        </div>
    )
}
export default App;