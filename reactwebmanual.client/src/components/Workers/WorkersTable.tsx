import { FC } from 'react';
import { IWorker } from '../../models/IWorker';
import {Button} from 'antd'
import {FormOutlined , DeleteOutlined } from '@ant-design/icons';

interface IAppProps {
    workers: IWorker[];
    editWorker: (id: number) => void;
    deleteWorker: (id: number) => void;
}

const WorkersTable: FC<IAppProps> = ({ workers, editWorker, deleteWorker }) => {

    return (
        <table className="tableWorker table-striped" aria-labelledby="tabelLabel">
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
                                <Button onClick={() => editWorker(worker.id)} icon={<FormOutlined />}/>
                                <Button onClick={() => deleteWorker(worker.id)} icon={<DeleteOutlined />}/>
                            </div>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>
    );
};

export default WorkersTable;