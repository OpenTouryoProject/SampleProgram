import './css/site.css';
import 'react-bootstrap';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import { BrowserRouter } from 'react-router-dom';
import * as RoutesModule from './routes';
import registerServiceWorker from './registerServiceWorker';

import { Provider } from 'react-redux';
import createStore from './createStore';

let routes = RoutesModule.routes;
function renderApp() {

    const store = createStore();
    // ★★★★★★★★★★
    console.log(JSON.stringify(store.getState()));

    // This code starts up the React app when it runs in a browser. It sets up the routing
    // configuration and injects the app into a DOM element.
    const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
    ReactDOM.render(
        <Provider store={store}>
            <AppContainer>
                <BrowserRouter children={ routes } basename={ baseUrl } />
            </AppContainer>
        </Provider>,
        document.getElementById('react-app'));
}
renderApp();
// Allow Hot Module Replacement
if (module.hot) {
    module.hot.accept('./routes', () => {
        routes = require('./routes').routes;
        renderApp();
    });
}

registerServiceWorker();