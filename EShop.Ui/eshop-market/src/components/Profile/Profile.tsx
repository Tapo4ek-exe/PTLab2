import {
  Avatar,
  Button,
  makeStyles,
  Subtitle2,
} from "@fluentui/react-components";
import React, { useEffect, useState } from "react";
import { EShopApi } from "../../api/EShopApi";

const useStyles = makeStyles({
  profile: {
    display: "flex",
    flexDirection: "row",
  },
  avatar: {
    marginRight: "10px",
  },
  name: {
    marginRight: "10px",
    marginTop: "auto",
    marginBottom: "auto",
  },
});

type ProfileProps = {
  onLogout?: () => void;
};

const Profile: React.FunctionComponent<ProfileProps> = ({ onLogout }) => {
  const classes = useStyles();
  const [name, setName] = useState("");
  const api = new EShopApi();

  const getUserName = async () => {
    const response = await api.getUserName();
    if (response.IsSuccess) {
      setName(response.Data ?? "");
    } else {
      console.error(response.Errors);
    }
  };

  const logout = async () => {
    await api.logout();
    if (onLogout) {
      onLogout();
    }
  };

  useEffect(() => {
    getUserName();
  }, []);

  return (
    <div className={classes.profile}>
      <Avatar className={classes.avatar} />
      <Subtitle2 className={classes.name}>{name}</Subtitle2>
      <Button appearance="secondary" onClick={logout}>
        Выйти
      </Button>
    </div>
  );
};

export { Profile };
