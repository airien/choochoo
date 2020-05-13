import * as React from 'react';
import { BrowserRouter, Route } from 'react-router-dom'
import Layout from './components/Layout';
import Home from './components/Home';
import InformationAdmin from './components/InformationAdmin';
import Information from './components/Information';

import './custom.css'

export default () => (
    <BrowserRouter>
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/admin' component={InformationAdmin} />
        <Route path='/info/:id?' component={Information} />
    </Layout>
    </BrowserRouter>
);
