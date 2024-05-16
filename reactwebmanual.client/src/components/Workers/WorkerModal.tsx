import { Dispatch, FC, FormEvent, SetStateAction, useCallback, useEffect, useMemo, useState } from "react";
import Modal from 'react-modal';
import { IWorker } from "../../models/IWorker";
import '../Modal.css';
import { ActionType } from "../../types/ActionType";

interface IWorkerModalProps {
    actionType: ActionType,
    setActionType: Dispatch<SetStateAction<ActionType>>;
    workerId: number | undefined,
    selectedDivisionId: number | undefined;
}

const WorkerModal: FC<IWorkerModalProps> = ({
    actionType,
    setActionType,
    workerId,
    selectedDivisionId
}) => {

    const [worker, setWorker] = useState<IWorker>();
    const [visible, setVisible] = useState(false);

    const getAndSetWorkerById = async (id: number) => {
        try {
            const response = await fetch(`https://localhost:7226/api/Worker/${id}`);
            const data = await response.json();
            console.log('data', data);
            setWorker(data);
        } catch {
            throw Error('ошибка');
        }
    };

    useEffect(() => {
        if (actionType == ActionType.Add) {
            const newWorker: IWorker = {
                id: 0,
                firstName: '',
                lastName: '',
                middleName: '',
                dateBithday: '1999-04-05T00:00:00',
                sex: '',
                post: '',
                driversLicense: true,
                divisionId: selectedDivisionId ?? 0,
            };

            setWorker(newWorker);
            setVisible(true);
            return;
        }

        if (actionType == ActionType.Edit && workerId) {
            getAndSetWorkerById(workerId);
            setVisible(true);
            return;
        }

        setVisible(false);
        setWorker(undefined);

    }, [actionType, workerId, selectedDivisionId])

    const closeModal = () => {
        setActionType(ActionType.None);
    };

    const formSubmit = useCallback(async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        e.stopPropagation();

        const newWorker: IWorker = {
            ...worker,
            id: worker ? worker.id : 0,
            lastName: e.target.elements['lastName'].value,
            firstName: e.target.elements['firstName'].value,
            middleName: e.target.elements['middleName'].value,
            dateBithday: e.target.elements['dateBithday'].value,
            sex: e.target.elements['sex'].value,
            post: e.target.elements['post'].value,
            driversLicense: e.target.elements['driversLicense'].value === 'true',
            divisionId: e.target.elements['divisionId'].value,

        };
        console.log('e', e);
        console.log('worker', newWorker);


        const response = await fetch(`https://localhost:7226/api/Worker`, {

            method: actionType == ActionType.Edit ? 'PUT' : 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newWorker)
        });
        if (!response.ok) {
            alert('Не удалось добавить');
            return;
        }

        setActionType(ActionType.Refresh);

    }, [worker, actionType]);

    const modalContent = useMemo(() => {
        console.log('worker', worker);
        return !!worker && (
            <div className='modalContent'>
                <button className="modalContentHeaderButton" onClick={closeModal}>Закрыть</button>
                <h2 >Добавить работника</h2>
                <form className="modalContentForm" onSubmit={formSubmit}>
                    <p>Фамилия</p>
                    <input name='lastName' required minLength={2} defaultValue={worker.lastName} ></input>
                    <p>Имя</p>
                    <input name='firstName' required minLength={2} defaultValue={worker.firstName} ></input>
                    <p>Отчество</p>
                    <input name='middleName' defaultValue={worker?.middleName} ></input>
                    <p>Дата Рождения</p>
                    <input name='dateBithday' required defaultValue={worker.dateBithday} ></input>
                    <p>Пол</p>
                    <select name='sex' required defaultValue={worker.sex} >
                        <option value=''></option>
                        <option value='муж'>Муж</option>
                        <option value='жен'>Жен</option>
                    </select>
                    <p>Должность</p>
                    <input name='post' required defaultValue={worker.post} ></input>
                    <p>Наличие прав</p>
                    <select name='driversLicense' required defaultValue={String(worker.driversLicense)}>
                        <option value="true">Есть</option>
                        <option value="false">Нет</option>
                    </select>
                    <p>Подразделение</p>
                    <input name="divisionId" required defaultValue={worker.divisionId}></input>

                    <button type='submit' className="modalContentButton">{
                        actionType == ActionType.Edit
                            ? 'Редактировать'
                            : 'Добавить'
                    }</button>
                </form>
            </div>
        );
    }, [formSubmit, worker, actionType]);

    return (
        <Modal isOpen={visible} onRequestClose={closeModal} ariaHideApp={false}>
            {modalContent}
        </Modal>
    )
};

export default WorkerModal;