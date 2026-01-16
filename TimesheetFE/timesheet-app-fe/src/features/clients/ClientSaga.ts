import { call, put, select, takeEvery } from 'redux-saga/effects'
import { Client } from './models/Client';
import { create, deleteClientByID, edit, getAllClients, getPage } from './ClientService';
import { clientsFetched, createClient, deleteClient, editClient, getClientsPage } from './ClientsSlice';
import { RootState } from '../../store';
import { Filter } from '../../common/classes/Filter';
import { ClientsPage } from './models/ClientsPage';

const getFilters = (state: RootState) => state.clients.filter;
const getNewClient = (state: RootState) => state.clients.newClient;
const getEditableClient = (state: RootState) => state.clients.editableClient;
const getDeleteClientId = (state: RootState) => state.clients.deleteClientID;

function* workGetClientsFetch() {
    const filteri: Filter = yield select(getFilters)
    const clientsPageResponse: { data: { totalPagesCount: number, items: [] } } = yield call(
        getPage, filteri.firstLetter, filteri.name, filteri.pageSize, filteri.pageNumber);
    const clientsPage: ClientsPage = new ClientsPage(clientsPageResponse.data.totalPagesCount, clientsPageResponse.data.items)
    yield put(clientsFetched(clientsPage));
}

function* workCreateClient() {
    const client: Client = yield select(getNewClient)
    yield call(create, client);
}

function* workEditClient() {
    const client: Client = yield select(getEditableClient)
    yield call(edit, client);
}

function* workDeleteClient() {
    const deleteClientId: string = yield select(getDeleteClientId)
    yield call(deleteClientByID, deleteClientId);
}


//takeEvery pregledaj takeLast etc.
export function* getClientPagesSaga() {
    yield takeEvery(getClientsPage.type, workGetClientsFetch)
}

export function* createClientSaga() {
    yield takeEvery(createClient.type, workCreateClient)
}

export function* editClientSaga() {
    yield takeEvery(editClient.type, workEditClient)
}

export function* deleteClientSaga() {
    yield takeEvery(deleteClient.type, workDeleteClient)
}

