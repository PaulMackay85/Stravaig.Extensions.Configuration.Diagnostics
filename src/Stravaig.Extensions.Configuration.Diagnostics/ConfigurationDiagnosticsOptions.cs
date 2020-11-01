using System;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    /// <summary>
    /// A container for options for the configuration diagnostics.
    /// </summary>
    public class ConfigurationDiagnosticsOptions
    {
        private ISecretObuscator _obfuscator = PlainTextObfuscator.Instance;
        private IMatcher _configurationKeyMatcher = NullMatcher.Instance;
        private IMatcher _connectionStringElementMatcher = NullMatcher.Instance;
            
        public ConfigurationDiagnosticsOptions GlobalOptions { get; } = new ConfigurationDiagnosticsOptions();

        public ISecretObuscator Obfuscator
        {
            get => _obfuscator;
            set => _obfuscator = value ?? PlainTextObfuscator.Instance;
        }

        public IMatcher ConfigurationKeyMatcher
        {
            get => _configurationKeyMatcher;
            set => _configurationKeyMatcher = value ?? NullMatcher.Instance;
        }

        public IMatcher ConnectionStringElementMatcher
        {
            get => _connectionStringElementMatcher;
            set => _connectionStringElementMatcher = value ?? NullMatcher.Instance;
        }

        public ConfigurationDiagnosticsOptions BuildConfigurationKeyMatcher(Action<IKeyMatchBuilder> builder)
        {
            KeyMatchBuilder result = new KeyMatchBuilder();
            result.Add(ConfigurationKeyMatcher);
            builder(result);
            ConfigurationKeyMatcher = result.Build();
            return this;
        }

        public ConfigurationDiagnosticsOptions BuildConnectionStringElementMatcher(Action<IKeyMatchBuilder> builder)
        {
            KeyMatchBuilder result = new KeyMatchBuilder();
            result.Add(ConnectionStringElementMatcher);
            builder(result);
            ConnectionStringElementMatcher = result.Build();
            return this;
        }
    }
}