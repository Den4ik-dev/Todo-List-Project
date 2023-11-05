// user => { login: 'den', password: 'den12345', accessToken, refreshToken }

import { createContext, useState } from 'react';

export const AuthContext = createContext(/* default value */);

const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  return (
    <AuthContext.Provider value={{ user, setUser }}>
      {children}
    </AuthContext.Provider>
  );
};
export default AuthProvider;
