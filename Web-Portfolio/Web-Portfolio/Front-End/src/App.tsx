import React from "react";
import "./App.css";

const App = () => {
  return (
    <div className="wrapper">
      <div className="header">
        <h1 className="title">EEE Portfolio</h1>
        <div className="goto">
          <h3 className="projects">Projects</h3>
          <h3 className="blogs">Blogs</h3>
          <h3 className="about">About</h3>
        </div>
      </div>
      <div className="main-section">
        <div className="body-main">Main body contents</div>
        <div className="nav-bar">Navigation</div>
      </div>
    </div>
  );
};

export default App;
