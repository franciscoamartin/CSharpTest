import React from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

import Company from './pages/Company';
import Supplier from './pages/Supplier';

export default function Routes() {
  return (
    <BrowserRouter>
      <Switch>
        <Route path="/" exact component={Supplier} />
        <Route path="/company" component={Company} />
      </Switch>
    </BrowserRouter>
  );
}
