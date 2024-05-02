import { FC, useCallback, useEffect, useId, useState } from 'react';
import Modal from 'react-modal';
import './Workers.css';

interface IAppProps {

}

interface IWorker {
    id: number,
    firstName: string,
    lastName: string,
    middleName: string,
    dateBithday: number,
    sex: string,
    post: string,
    driversLicense: boolean,
}

const Workers: FC<IAppProps> = ({ }) => {
    const [workers, setWorkers] = useState<IWorker[]>();
    const [modalIsOpen, setModalIsOpen] = useState(false);

    const openModal = () => {
        setModalIsOpen(true);
    };

    const closeModal = () => {
        setModalIsOpen(false)
    };

    const modalContent = (
        <div>
            <h2>Модалка</h2>
            <p>текст</p>
            <button onClick={closeModal}>Закрыть</button>
        </div>
    )

    const getWorkers = useCallback(async () => {
        const response = await fetch('https://localhost:7226/api/Worker');

        const data = await response.json();
        setWorkers(data);
    }, [setWorkers]);



    useEffect(() => {
        getWorkers();
    }, [getWorkers]);

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
                <button className='workersButton'onClick={() => openModal()}>ADD Worker</button>
                <Modal isOpen={modalIsOpen} onRequestClose={closeModal}>
                    {modalContent}
                </Modal>
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
                                <button className='buttonDelete' onClick={() => deleteWorker(worker.id)}>X</button>
                                <button className='buttonUpdate'></button>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
};
export default Workers;