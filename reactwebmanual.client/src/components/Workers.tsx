import { FC, FormEvent, useCallback, useEffect, useId, useState, Dispatch, SetStateAction } from 'react';
import './Workers.css';
import { IWorker } from '../models/IWorker';
import WorkerModal from './WorkerModal';

interface IAppProps {
    selectedDivisions: string | undefined// пока не понятно
}

const Workers: FC<IAppProps> = ({ selectedDivisions }) => {

    const [workers, setWorkers] = useState<IWorker[]>();
    const [currentWorker, setCurrentWorker] = useState<IWorker | null>(null);
    const [modalIsOpen, setModalIsOpen] = useState(false);

    const getWorkers = useCallback(async () => {
        const response = await fetch('https://localhost:7226/api/Worker/');

        const data = await response.json();
        setWorkers(data);
    }, [setWorkers]);

    useEffect(() => {
        getWorkers();
    }, [getWorkers]);

    const addWorker = useCallback(() => {
        setCurrentWorker(null);
        setModalIsOpen(true);
    }, [setCurrentWorker, setModalIsOpen]);

    const editWorker = useCallback((worker: IWorker) => {
        setCurrentWorker(worker);
        setModalIsOpen(true);
    }, [setCurrentWorker, setModalIsOpen]);

    const deleteWorker = useCallback(async (id: number) => {
        const response = await fetch(`https://localhost:7226/api/Worker/${id}`, { method: 'DELETE' });
        if (!response.ok) {
            console.log('Не удалось удалить');

            return;
        }

        await getWorkers();
    }, [getWorkers]);

    return (
        <div>
            <div className='workers'>
                <h1 className='workers'>Работники</h1>
                <button className='workersButton' onClick={addWorker} >ADD Worker</button>
                <WorkerModal worker={currentWorker} modalIsOpen={modalIsOpen} setModalIsOpen={setModalIsOpen} />
            </div>
            <table className="table table-striped" aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>firstName</th>
                        <th>lastName</th>
                        <th>middleName</th>
                        <th>dateBithday</th>
                        <th>sex</th>
                        <th>post</th>
                        <th>driversLicense</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {workers?.map(worker =>
                        <tr key={worker.id}>
                            <td>{worker.firstName}</td>
                            <td>{worker.lastName}</td>
                            <td>{worker.middleName}</td>
                            <td>{worker.dateBithday}</td>
                            <td>{worker.sex}</td>
                            <td>{worker.post}</td>
                            <td>{worker.driversLicense ? 'Есть' : 'Нет'}</td>
                            <td>
                                <div className='buttons'>
                                    <button className='buttonDelete' onClick={() => deleteWorker(worker.id)}>X</button>
                                    <button className='buttonUpdate' onClick={() => editWorker(worker)}></button>
                                </div>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
};
export default Workers;