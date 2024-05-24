import { Dispatch, FC, FormEvent, SetStateAction, useCallback, useEffect, useMemo, useState } from "react";
import Modal from 'react-modal';
import { IWorker } from "../../models/IWorker";
import '../Modal.css';
import { ActionType } from "../../types/ActionType";
import TreeSelectDivision from '../Divisions/selectDivision';
import { DatePicker, DatePickerProps } from 'antd';
import dayjs from 'dayjs';
import customParseFormat from 'dayjs/plugin/customParseFormat';


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
    const [value, setValue] = useState<string>();
    const [worker, setWorker] = useState<IWorker>();
    const [visible, setVisible] = useState(false);
    const [datee, setDatee] = useState<string | string[]>('');

    dayjs.extend(customParseFormat);

    const dateFormat = 'YYYY-MM-DD';


    const getAndSetWorkerById = async (id: number) => {
        try {
            const response = await fetch(`https://localhost:7226/api/Worker/${id}`);
            const data = await response.json();
            setWorker(data);
        } catch {
            throw Error('ошибка');
        }
    };

    const handleChangeDateBithday: DatePickerProps['onChange'] = (date, dateString) => {
        setDatee(dateString);
    };

    const handleChangeDivisionId = (divisionId: string | undefined) => {
        setWorker((prevState) => {
            if (!prevState)
                return prevState;
            return {
                ...prevState,
                divisionId: divisionId as unknown as number,
            }
        });
    };

    useEffect(() => {
        if (actionType == ActionType.Add) {
            const newWorker: IWorker = {
                id: 0,
                firstName: '',
                lastName: '',
                middleName: '',
                dateBithday: '2000-01-01',
                gender: NaN,
                post: '',
                isDriversLicense: true,
                divisionId: selectedDivisionId ?? 0,
            };

            setWorker(newWorker);
            setValue(String(newWorker.divisionId));
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
            gender: e.target.elements['gender'].value,
            post: e.target.elements['post'].value,
            isDriversLicense: e.target.elements['isDriversLicense'].value === 'true',
            divisionId: Number(worker?.divisionId),
        };

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

    }, [worker, actionType, value]);

    const modalContent = useMemo(() => {
        return !!worker && (
            <div className='modalContent'>
                <h2 >{worker.id ? 'Редактирование работника' : 'Добавить работника'}</h2>
                <form className="modalContentForm" onSubmit={formSubmit}>
                    <p>Фамилия</p>
                    <input name='lastName' required minLength={2} defaultValue={worker.lastName} ></input>
                    <p>Имя</p>
                    <input name='firstName' required minLength={2} defaultValue={worker.firstName} ></input>
                    <p>Отчество</p>
                    <input name='middleName' defaultValue={worker?.middleName} ></input>
                    <p>Дата Рождения(ГГГГ-ММ-ДД)</p>
                    {/* <input name='dateBithday' required defaultValue={worker.dateBithday} ></input> */}
                    <DatePicker
                        name='dateBithday'
                        defaultValue={dayjs(String(worker.dateBithday), dateFormat)}
                        onChange={handleChangeDateBithday}
                    />
                    <p>Пол</p>
                    <select name='gender' required defaultValue={worker.gender} >
                        <option value=''></option>
                        <option value='0' >Муж</option>
                        <option value='1'>Жен</option>
                    </select>
                    <p>Должность</p>
                    <input name='post' required defaultValue={worker.post} ></input>
                    <p>Наличие прав</p>
                    <select name='isDriversLicense' required defaultValue={String(worker.isDriversLicense)}>
                        <option value="true">Есть</option>
                        <option value="false">Нет</option>
                    </select>
                    <p>Подразделение</p>
                    <TreeSelectDivision onSelect={handleChangeDivisionId} value={worker.divisionId} />

                    <button type='submit' className="modalContentButton">Cохранить</button>
                </form>
                <button className="modalContentButton" onClick={closeModal}>Отмена</button>
            </div>
        );
    }, [formSubmit, worker, actionType, value]);

    return (
        <Modal isOpen={visible} onRequestClose={closeModal} ariaHideApp={false}>
            {modalContent}
        </Modal>
    )
};

export default WorkerModal;