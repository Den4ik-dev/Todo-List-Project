import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Home from '../Home/Home';
import Login from '../Login/Login';
import Registration from '../Registration/Registration';

const Router = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<Home />} path="/" />
        <Route element={<Login />} path="/login" />
        <Route element={<Registration />} path="/registration" />
      </Routes>
    </BrowserRouter>
  );
};

export default Router;
