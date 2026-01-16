import ReactDOM from 'react-dom/client';
import App from './App';
import './index.css';
import { Provider } from 'react-redux';
import { store } from './store';
import NovaKomp from './TestCategory';
import TestClient from './TestClient';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <Provider store={store}>
    <NovaKomp/>
    <TestClient/>
  </Provider>
);

