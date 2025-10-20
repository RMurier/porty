import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import AppBar from "./components/AppBar";
// import Projects from "./pages/Projects";
// import Skills from "./pages/Skills";
// import Contact from "./pages/Contact";
// import SignIn from "./pages/SignIn";
// import SignUp from "./pages/SignUp";
import "./index.css";
import Home from "./pages/Home";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <BrowserRouter>
      <AppBar />
      <div className="pt-14">  {/* padding = hauteur de AppBar pour que le contenu ne soit pas caché */}
        <Routes>
          <Route path="/" element={<Home />} />
          {/* <Route path="/projects" element={<Projects />} />
          <Route path="/skills" element={<Skills />} />
          <Route path="/contact" element={<Contact />} />
          <Route path="/sign-in" element={<SignIn />} />
          <Route path="/sign-up" element={<SignUp />} /> */}
          {/* autres routes */}
        </Routes>
      </div>
    </BrowserRouter>
  </React.StrictMode>
);
