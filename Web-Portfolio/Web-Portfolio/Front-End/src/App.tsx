import React, { Fragment, useState } from "react";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import GoogleLogin from "react-google-login";

export const App = () => {
  let [value, setValue] = useState("");
  let [rValue, setrValue] = useState("<h1>Default value</h1>");
  let dataisReady = false;

  const modules = {
    toolbar: [
      [{ header: [1, 2, false] }],
      ["bold", "italic", "underline", "strike", "blockquote"],
      [
        { list: "ordered" },
        { list: "bullet" },
        { indent: "-1" },
        { indent: "+1" },
      ],
      ["link", "image"],
      ["clean"],
    ],
  };

  const formats = [
    "header",
    "bold",
    "italic",
    "underline",
    "strike",
    "blockquote",
    "list",
    "bullet",
    "indent",
    "link",
    "image",
  ];

  const displayData = async () => {
    const test = await fetch(
      "https://localhost:44366/api/blogs/5f11b43b62d23a46846fca5d"
    );

    const ddd = await test.json();
    setrValue(ddd.content);

    setTimeout(() => (dataisReady = true), 5000);

    console.log("logging");
  };

  const saveData = async () => {
    const data = {
      Content: value,
      Type: 9,
      Date: new Intl.DateTimeFormat("en-GB", {
        year: "numeric",
        month: "long",
        day: "2-digit",
      }).format(new Date()),
    };

    await fetch("https://localhost:44366/api/blogs", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
    console.log(data);
  };

  const responseGoogle = (res: any) => {
    console.log(res.googleId);

    if (res.googleId == "116635555336829576978") {
      console.log("give perm");
    }
  };

  const loginHandler = async () => {
    const user = {
      Username: "masterdu",
      Password: "masterdu",
    };

    await fetch("https://localhost:44366/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(user),
    });
  };

  return (
    <Fragment>
      <h1>Main Page</h1>
      <ReactQuill
        theme="snow"
        value={value}
        onChange={(val) => {
          setValue(val);
        }}
        modules={modules}
        formats={formats}
      />
      <button onClick={saveData}>Save Data</button>
      <button onClick={displayData}>Display Data</button>
      <button onClick={loginHandler}>Loggin</button>
      {dataisReady ? (
        <h1>content loading</h1>
      ) : (
        <div dangerouslySetInnerHTML={{ __html: rValue }} />
      )}
      <GoogleLogin
        clientId="414639654695-3mibqbdb0g2c02pg3kshtmapg56vccqb.apps.googleusercontent.com"
        buttonText="Login with Google"
        onSuccess={responseGoogle}
        onFailure={responseGoogle}
      />
    </Fragment>
  );
};
