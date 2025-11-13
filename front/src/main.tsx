import './i18n';
import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import AppBar from "./components/AppBar";
import "./index.css";
import Home from "./pages/Home";
import SignIn from "./pages/SignIn";
import SignUp from "./pages/SignUp";
import EmailConfirmation from './pages/EmailConfirmation';
import LocaleSwitcher from './components/LocaleSwitcher';

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <BrowserRouter>
      <AppBar />
      <LocaleSwitcher />
      <div className="pt-14">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/sign-in" element={<SignIn />} />
          <Route path="/sign-up" element={<SignUp />} />
          <Route path="/email-confirmation" element={<EmailConfirmation />} />
        </Routes>
      </div>
    </BrowserRouter>
  </React.StrictMode>
);
