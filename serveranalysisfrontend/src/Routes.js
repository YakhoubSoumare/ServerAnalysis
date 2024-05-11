import React from "react";
import { Route, Switch } from "react-router-dom";

import Home from "./components/Home";
import Thesis from "./components/Thesis";
import About from "./components/About";

const Routes = () => {
    return (
        <Switch>
            <Route exact path="/" component={Home} />
            <Route path="/thesis" component={Thesis} />
            <Route path="/about" component={About} />
        </Switch>
    );
}

export default Routes;