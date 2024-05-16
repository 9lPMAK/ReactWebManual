import { FC } from 'react';
import { IWorker } from '../../models/IWorker';

interface IAppProps {
    workers: IWorker[];
    editWorker: (id: number) => void;
    deleteWorker: (id: number) => void;
}

const WorkersTable: FC<IAppProps> = ({ workers, editWorker, deleteWorker }) => {

    return (
        <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Имя</th>
                    <th>Фамилия</th>
                    <th>Отчество</th>
                    <th>Дата Рождения</th>
                    <th>Пол</th>
                    <th>Должность</th>
                    <th>ВУ</th>
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
                                <button className='buttonUpdate' onClick={() => editWorker(worker.id)}></button>
                                <button className='buttonDelete' onClick={() => deleteWorker(worker.id)}>X</button>
                            </div>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>
    );
};

export default WorkersTable;