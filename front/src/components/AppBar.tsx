import React, { useEffect, useMemo, useRef, useState } from "react";
import { Link, useNavigate, useLocation } from "react-router-dom";
import { Menu, X, User } from "lucide-react";
import { motion, AnimatePresence } from "framer-motion";
import { useTranslation } from 'react-i18next';

type NavLink = { label: string; href: string };

const links: NavLink[] = [
  { label: "Header.categories.home", href: "/" },
  { label: "Header.categories.projects", href: "/projects" },
  { label: "Header.categories.skills", href: "/skills" },
  { label: "Header.categories.contact", href: "/contact" },
];

const AppBar: React.FC = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const { pathname } = useLocation();

  // Auth simulée. Branche sur ta vraie auth.
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [userName, setUserName] = useState<string>("");

  // UI state
  const [elevated, setElevated] = useState(false);
  const [mobileOpen, setMobileOpen] = useState(false);
  const [menuOpen, setMenuOpen] = useState(false);

  const menuRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const onScroll = () => setElevated(window.scrollY > 4);
    onScroll();
    window.addEventListener("scroll", onScroll, { passive: true });
    return () => window.removeEventListener("scroll", onScroll);
  }, []);

  useEffect(() => {
    const onClickAway = (e: MouseEvent) => {
      if (menuRef.current && !menuRef.current.contains(e.target as Node)) setMenuOpen(false);
    };
    document.addEventListener("mousedown", onClickAway);
    return () => document.removeEventListener("mousedown", onClickAway);
  }, []);

  useEffect(() => {
    setMobileOpen(false);
    setMenuOpen(false);
  }, [pathname]);

  useEffect(() => {
    const onKey = (e: KeyboardEvent) => {
      if (e.key === "Escape") {
        setMobileOpen(false);
        setMenuOpen(false);
      }
    };
    document.addEventListener("keydown", onKey);
    return () => document.removeEventListener("keydown", onKey);
  }, []);

  const initials = useMemo(
    () => (userName?.trim() ? userName.trim()[0].toUpperCase() : "U"),
    [userName]
  );


  const handleLogin = () => {
    navigate("/sign-in");
  };
  const handleLogout = () => {
    setIsAuthenticated(false);
    setUserName("");
    navigate("/");
  };

  return (
    <header
      className={[
        "sticky top-0 inset-x-0 z-50",
        "bg-white/80 dark:bg-zinc-950/70 backdrop-blur",
        "border-b border-zinc-200 dark:border-zinc-800",
        elevated ? "shadow-sm" : "",
      ].join(" ")}
      role="banner"
    >
      <nav className="px-4 sm:px-6 lg:px-8" aria-label="Navigation principale">
        <div className="h-16 flex items-center justify-between">
          <Link to="/" className="flex items-center gap-2 min-w-0">
            <img
              src="/assets/logo.png"
              alt="Logo Porty"
              className="h-8 w-8 rounded-full object-cover"
            />
            <span className="truncate text-lg sm:text-xl font-semibold dark:text-white">Porty</span>
          </Link>

          {/* Liens desktop */}
          <div className="hidden md:flex items-center gap-6">
            {links.map((l) => {
              const active = pathname === l.href;
              return (
                <Link
                  key={l.href}
                  to={l.href}
                  className={[
                    "text-sm sm:text-base font-medium transition",
                    active
                      ? "text-blue-700 dark:text-blue-400"
                      : "text-gray-700 dark:text-gray-300 hover:text-blue-600 dark:hover:text-blue-400",
                  ].join(" ")}
                >
                  {t(l.label)}
                </Link>
              );
            })}
          </div>
          <div className="flex items-center gap-2">
            <div className="relative" ref={menuRef}>
              <button
                type="button"
                aria-haspopup="menu"
                aria-expanded={menuOpen}
                onClick={() => setMenuOpen((v) => !v)}
                className="h-10 w-10 rounded-full border border-zinc-300 dark:border-zinc-700 bg-white/60 dark:bg-zinc-900/60 flex items-center justify-center"
                title={isAuthenticated ? userName : "Compte"}
              >
                <User className="h-5 w-5" color="currentColor" strokeWidth={2} />
                <span className="sr-only">Ouvrir le menu compte</span>
              </button>

              <AnimatePresence>
                {menuOpen && (
                  <motion.div
                    role="menu"
                    initial={{ opacity: 0, y: -6 }}
                    animate={{ opacity: 1, y: 0 }}
                    exit={{ opacity: 0, y: -6 }}
                    transition={{ duration: 0.16 }}
                    className="absolute right-0 mt-2 w-56 overflow-hidden rounded-xl border border-zinc-200 dark:border-zinc-700 bg-white dark:bg-zinc-900 shadow-md"
                  >
                    <div className="px-4 py-3 text-sm text-zinc-600 dark:text-zinc-300">
                      {isAuthenticated ? (
                        <span className="flex items-center gap-2">
                          <span className="inline-flex h-6 w-6 items-center justify-center rounded-full bg-zinc-200 dark:bg-zinc-800 text-xs font-semibold">
                            {initials}
                          </span>
                          {userName}
                        </span>
                      ) : (
                        t('Header.guest')
                      )}
                    </div>
                    <div className="h-px bg-zinc-200 dark:bg-zinc-800" />
                    {isAuthenticated ? (
                      <>
                        <Link
                          role="menuitem"
                          to="/me"
                          className="block px-4 py-2 text-sm hover:bg-zinc-100 dark:hover:bg-zinc-800"
                        >
                          {t('Header.account')}
                        </Link>
                        <Link
                          role="menuitem"
                          to="/projects"
                          className="block px-4 py-2 text-sm hover:bg-zinc-100 dark:hover:bg-zinc-800"
                        >
                          Mes projets
                        </Link>
                        <button
                          role="menuitem"
                          onClick={handleLogout}
                          className="w-full text-left px-4 py-2 text-sm text-red-600 hover:bg-red-50 dark:hover:bg-red-900/20"
                        >
                          {t('Header.logout')}
                        </button>
                      </>
                    ) : (
                      <>
                        <button
                          role="menuitem"
                          onClick={handleLogin}
                          className="w-full text-left px-4 py-2 text-sm hover:bg-zinc-100 dark:hover:bg-zinc-800"
                        >
                          {t('Header.signin')}
                        </button>
                        <button
                          role="menuitem"
                          onClick={() => navigate("/sign-up")}
                          className="w-full text-left px-4 py-2 text-sm hover:bg-zinc-100 dark:hover:bg-zinc-800"
                        >
                          {t('Header.signup')}
                        </button>
                      </>
                    )}
                  </motion.div>
                )}
              </AnimatePresence>
            </div>

            {/* Burger mobile */}
            <button
              type="button"
              className="md:hidden h-10 w-10 inline-flex items-center justify-center"
              aria-label="Ouvrir le menu"
              aria-controls="mobile-nav"
              aria-expanded={mobileOpen}
              onClick={() => setMobileOpen((v) => !v)}
            >
              {mobileOpen ? (
                <X className="h-6 w-6 text-zinc-800 dark:text-zinc-200" />
              ) : (
                <Menu className="h-6 w-6 text-zinc-800 dark:text-zinc-200" />
              )}
            </button>
          </div>
        </div>
      </nav>

      {/* Menu mobile */}
      <AnimatePresence>
        {mobileOpen && (
          <motion.div
            id="mobile-nav"
            initial={{ opacity: 0, y: -8 }}
            animate={{ opacity: 1, y: 0 }}
            exit={{ opacity: 0, y: -8 }}
            transition={{ duration: 0.18 }}
            className="md:hidden border-t border-zinc-200 dark:border-zinc-800 bg-white dark:bg-zinc-950"
          >
            <div className="px-4 py-2">
              {links.map((l) => {
                const active = pathname === l.href;
                return (
                  <Link
                    key={l.href}
                    to={l.href}
                    className={[
                      "block px-2 py-3 text-base rounded-md",
                      active
                        ? "text-blue-700 dark:text-blue-400 bg-blue-50 dark:bg-blue-950/30"
                        : "text-zinc-800 dark:text-zinc-200 hover:bg-zinc-100 dark:hover:bg-zinc-900",
                    ].join(" ")}
                    onClick={() => setMobileOpen(false)}
                  >
                    {l.label}
                  </Link>
                );
              })}

              <div className="h-px my-2 bg-zinc-200 dark:bg-zinc-800" />

              {isAuthenticated ? (
                <>
                  <Link
                    to="/me"
                    className="block px-2 py-3 text-base rounded-md hover:bg-zinc-100 dark:hover:bg-zinc-900"
                  >
                    Mon compte
                  </Link>
                  <button
                    onClick={handleLogout}
                    className="w-full text-left px-2 py-3 text-base rounded-md text-red-600 hover:bg-red-50 dark:hover:bg-red-900/20"
                  >
                    Déconnexion
                  </button>
                </>
              ) : (
                <>
                  <button
                    onClick={handleLogin}
                    className="w-full text-left px-2 py-3 text-base rounded-md hover:bg-zinc-100 dark:hover:bg-zinc-900"
                  >
                    {t('Header.signin')}
                  </button>
                  <button
                    onClick={() => navigate("/sign-up")}
                    className="w-full text-left px-2 py-3 text-base rounded-md hover:bg-zinc-100 dark:hover:bg-zinc-900"
                  >
                    {t('Header.signin')}
                  </button>
                </>
              )}
            </div>
          </motion.div>
        )}
      </AnimatePresence>
    </header>
  );
};

export default AppBar;
