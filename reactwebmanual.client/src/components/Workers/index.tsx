import { FC, useEffect, useState, } from 'react';
import './index.css';
import { IWorker } from '../../models/IWorker';
import WorkerModal from './WorkerModal';
import WorkersTable from './WorkersTable';
import { ActionType } from '../../types/ActionType';

interface IAppProps {
    selectedDivisionId: number | undefined
}

const Workers: FC<IAppProps> = ({ selectedDivisionId }) => {

    const [workers, setWorkers] = useState<IWorker[]>([]);
    const [selectedWorkerId, setSelectedWorkerId] = useState<number>();
    const [actionType, setActionType] = useState(ActionType.Refresh);

    const getAndFillWorkers = async (selectedDivisionId: number | undefined) => {
        if (selectedDivisionId === undefined) {
            setWorkers([]);
            return;
        }

        const response = await fetch(`https://localhost:7226/api/Worker/divisionId/${selectedDivisionId}`, { method: 'GET' });
        const data = await response.json();
        setWorkers(data);
        setActionType(ActionType.None);
    };

    useEffect(() => {
        if (actionType != ActionType.Refresh)
            return;

        getAndFillWorkers(selectedDivisionId);
    }, [selectedDivisionId, actionType]);

    useEffect(() => {
        getAndFillWorkers(selectedDivisionId);
    }, [selectedDivisionId]);

    const addWorker = () => {
        setSelectedWorkerId(undefined);
        setActionType(ActionType.Add);
    };

    const editWorker = (id: number) => {
        setSelectedWorkerId(id);
        setActionType(ActionType.Edit);
    };

    const deleteWorker = async (id: number) => {
        const response = await fetch(`https://localhost:7226/api/Worker/${id}`, { method: 'DELETE' });
        if (!response.ok) {
            alert('Не удалось удалить');
            return;
        }

        setActionType(ActionType.Refresh);
    };

    return (
        <div>
            <div className='workers'>
                <h1 className='workers'>Работники</h1>
                <button className='workersButton' onClick={addWorker} >Добавить</button>

            </div>
            <WorkersTable
                workers={workers}
                editWorker={editWorker}
                deleteWorker={deleteWorker}
            />
            <WorkerModal
                actionType={actionType}
                setActionType={setActionType}
                workerId={selectedWorkerId}
                selectedDivisionId={selectedDivisionId}
            />
        </div>
    );
};
export default Workers;