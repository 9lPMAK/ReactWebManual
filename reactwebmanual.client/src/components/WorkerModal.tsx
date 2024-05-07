import { FC, FormEvent, useCallback, useEffect, useMemo, useState } from "react";
import Modal from 'react-modal';
import { IWorker } from "../models/IWorker";
import './WorkerModal.css';

interface IWorkerModalProps {
    worker?: IWorker | null,
    modalIsOpen: boolean,
    setModalIsOpen: (value: boolean) => void
}

const WorkerModal: FC<IWorkerModalProps> = ({
    worker,
    modalIsOpen,
    setModalIsOpen
}) => {

    useEffect(() => {
        if (worker) {
            // TODO: Заполнить поля формы
        }
        console.log('worker', worker);

    }, [worker])

    const openModal = () => {
        setModalIsOpen(true);
    };

    const closeModal = () => {
        setModalIsOpen(false)
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
            driversLicense: Boolean(e.target.elements['driversLicense'].value),
        };
        console.log('e', e);
        console.log('worker', newWorker);
            
            
            const response = await fetch(`https://localhost:7226/api/Worker`, {
                
                method: worker ? 'PUT' : 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newWorker)
            });
            if (!response.ok) {
                console.log('Не удалось добавить');
                
                return;
            }
        
        closeModal();
    }, [worker]);

    const modalContent = useMemo(() => {
        return (
            <div className='modalContent'>
                <button className="modalContentHeaderButton" onClick={closeModal}>Закрыть</button>
                <h2 >Добавить работника</h2>
                <form className="modalContentForm" onSubmit={formSubmit}>
                    <p>Фамилия</p>
                    <input name='lastName' alt="lol" required minLength={2}></input>
                    <p>Имя</p>
                    <input name='firstName' required minLength={2}></input>
                    <p>Отчество</p>
                    <input name='middleName'></input>
                    <p>Дата Рождения</p>
                    <input name='dateBithday' required></input>
                    <p>Пол</p>
                    <select name='sex' required>
                        <option value=''></option>
                        <option value='муж'>Муж</option>
                        <option value='жен'>Жен</option>
                    </select>
                    <p>Должность</p>
                    <input name='post'></input>
                    <p>Наличие прав</p>
                    <select name='driversLicense' required>
                        <option value=""></option>
                        <option value="true">Есть</option>
                        <option value="false">Нет</option>
                    </select>

                    <button type='submit' className="modalContentButton">{
                        worker
                            ? 'Редактировать'
                            : 'Добавить'
                    }</button>
                </form>
            </div>
        );
    }, [formSubmit]);

    return (
        <Modal isOpen={modalIsOpen} onRequestClose={closeModal} ariaHideApp={false}>
            {modalContent}
        </Modal>
    )
};

export default WorkerModal;