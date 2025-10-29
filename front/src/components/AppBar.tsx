import React, { useState, useRef, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Menu, X } from "lucide-react";
import { motion, AnimatePresence } from "framer-motion";

type NavLink = { label: string; href: string };

const AppBar: React.FC = () => {
  const navigate = useNavigate();
  const [menuOpen, setMenuOpen] = useState(false);
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
  const menuRef = useRef<HTMLDivElement>(null);

  // Simule l’authentification
  const [isAuthentified, setIsAuthentified] = useState(false);
  const [userName, setUserName] = useState<string>("");

  useEffect(() => {
    const handler = (e: MouseEvent) => {
      if (menuRef.current && !menuRef.current.contains(e.target as Node)) {
        setMenuOpen(false);
      }
    };
    document.addEventListener("mousedown", handler);
    return () => document.removeEventListener("mousedown", handler);
  }, []);

  const links: NavLink[] = [
    { label: "Accueil", href: "/" },
    { label: "Projets", href: "/projects" },
    { label: "Compétences", href: "/skills" },
    { label: "Contact", href: "/contact" },
  ];

  const handleLogin = () => {
    // logique login
    setIsAuthentified(true);
    setUserName("Romain");
    navigate("/sign-in");
  };

  const handleSignUp = () => {
    navigate("/sign-up");
  };

  const handleLogout = () => {
    setIsAuthentified(false);
    setUserName("");
    navigate("/");
  };

  return (
    <nav className="fixed top-0 left-0 w-full bg-white dark:bg-zinc-900 border-b dark:border-zinc-800 z-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between items-center h-16">
          {/* Logo + nom */}
          <div className="flex items-center">
            <Link to="/" className="flex items-center gap-2">
              <img
                src="/assets/logo.png"
                alt="Porty logo"
                className="h-8 w-8 rounded-full object-cover"
              />
              <span className="text-xl font-semibold dark:text-white">Porty</span>
            </Link>
          </div>

          {/* Liens desktop */}
          <div className="hidden md:flex items-center space-x-6">
            {links.map((link) => (
              <Link
                key={link.href}
                to={link.href}
                className="text-lg font-medium text-gray-700 dark:text-gray-300 hover:text-blue-600 dark:hover:text-blue-400 transition"
              >
                {link.label}
              </Link>
            ))}
          </div>

          {/* Actions utilisateur + burger */}
          <div className="flex items-center space-x-4">
            {isAuthentified ? (
              <div className="relative flex items-center gap-2" ref={menuRef}>
                <span className="text-sm font-medium dark:text-gray-100">{userName}</span>
                <button
                  className="w-10 h-10 rounded-full bg-gray-200 dark:bg-zinc-800 hover:bg-gray-300 dark:hover:bg-zinc-700 flex items-center justify-center"
                  onClick={() => setMenuOpen(!menuOpen)}
                >
                  👤
                </button>
                <AnimatePresence>
                  {menuOpen && (
                    <motion.div
                      initial={{ opacity: 0, y: -8 }}
                      animate={{ opacity: 1, y: 0 }}
                      exit={{ opacity: 0, y: -8 }}
                      transition={{ duration: 0.2 }}
                      className="absolute right-0 mt-2 w-48 bg-white dark:bg-zinc-800 border dark:border-zinc-700 rounded-lg shadow-lg z-50"
                    >
                      <Link
                        to="/me"
                        className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-zinc-700"
                        onClick={() => setMenuOpen(false)}
                      >
                        Paramètres
                      </Link>
                      <Link
                        to="/projects"
                        className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-zinc-700"
                        onClick={() => setMenuOpen(false)}
                      >
                        Mes Projets
                      </Link>
                      <button
                        className="w-full text-left px-4 py-2 text-red-600 hover:bg-gray-100 dark:hover:bg-red-900/20"
                        onClick={() => {
                          setMenuOpen(false);
                          handleLogout();
                        }}
                      >
                        Se déconnecter
                      </button>
                    </motion.div>
                  )}
                </AnimatePresence>
              </div>
            ) : (
              <>
                <button
                  className="px-4 py-2 border border-blue-600 text-blue-600 rounded-md hover:bg-blue-50 transition hidden md:inline"
                  onClick={handleLogin}
                >
                  Se connecter
                </button>
                <button
                  className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition hidden md:inline"
                  onClick={handleSignUp}
                >
                  S’enregistrer
                </button>
              </>
            )}

            {/* Burger menu mobile */}
            <div className="md:hidden">
              <button
                onClick={() => setMobileMenuOpen(!mobileMenuOpen)}
                className="text-gray-700 dark:text-gray-200 focus:outline-none"
              >
                {mobileMenuOpen ? <X size={28} /> : <Menu size={28} />}
              </button>
            </div>
          </div>
        </div>
      </div>

      {/* Menu mobile déroulant */}
      <AnimatePresence>
        {mobileMenuOpen && (
          <motion.div
            initial={{ opacity: 0, y: -20 }}
            animate={{ opacity: 1, y: 0 }}
            exit={{ opacity: 0, y: -20 }}
            transition={{ duration: 0.3 }}
            className="md:hidden bg-white dark:bg-zinc-900 border-t dark:border-zinc-800 shadow-md py-2"
          >
            {links.map((link) => (
              <Link
                key={link.href}
                to={link.href}
                className="block px-4 py-2 text-lg text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-zinc-800"
                onClick={() => setMobileMenuOpen(false)}
              >
                {link.label}
              </Link>
            ))}
            {!isAuthentified && (
              <>
                <button
                  className="w-full text-left px-4 py-2 text-blue-600 hover:bg-gray-100 dark:text-blue-400"
                  onClick={() => {
                    setMobileMenuOpen(false);
                    handleLogin();
                  }}
                >
                  Se connecter
                </button>
                <button
                  className="w-full text-left px-4 py-2 bg-blue-600 text-white hover:bg-blue-700 transition"
                  onClick={() => {
                    setMobileMenuOpen(false);
                    handleSignUp();
                  }}
                >
                  S’enregistrer
                </button>
              </>
            )}
          </motion.div>
        )}
      </AnimatePresence>
    </nav>
  );
};

export default AppBar;

