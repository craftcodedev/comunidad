<?php


abstract class StringValueObject
{
    protected string $value;

    abstract protected function __construct(string $value);

    public function equals(self $stringValueObject)
    {
        return ($this->value() === $stringValueObject->value());
    }

    public static function fromString(string $value): static
    {
        return new static($value);
    }

    public function value(): string
    {
        return $this->value;
    }
}


final class UserFirstName extends StringValueObject
{
    public const MIN_LENGTH = 3;
    public const MAX_LENGTH = 40;

    public function __construct(string $value)
    {
        if (strlen($value) < self::MIN_LENGTH) {
            throw InvalidAttributeException::fromMinLength('first name', self::MIN_LENGTH);
        }

        if (strlen($value) > self::MAX_LENGTH) {
            throw InvalidAttributeException::fromMaxLength('first name', self::MAX_LENGTH);
        }

        $this->value = $value;
    }
}